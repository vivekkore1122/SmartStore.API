using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SmartStore.API.Data;
using SmartStore.API.Models.Domain;
using SmartStore.API.Models.DTO;
using SmartStore.API.Services.Interfaces;

namespace SmartStore.API.Services.Implementation;

public class AuthService : IAuthService
{
    private readonly SmartStoreDbContext dbContext;
    private readonly IConfiguration configuration;
    private readonly PasswordHasher<User> passwordHasher;

    public AuthService(SmartStoreDbContext dbContext, IConfiguration configuration)
    {
        this.dbContext = dbContext;
        this.configuration = configuration;
        passwordHasher = new PasswordHasher<User>();
    }

    private int GetAccessTokenExpiryMinutes()
    {
        return configuration.GetValue<int>(
            "Jwt:ExpiryMinutes");
    }

    private int GetRefreshTokenExpiryDays()
    {
        return configuration.GetValue<int>(
            "Jwt:RefreshTokenExpiryDays");
    }

    private string GenerateRefreshToken()
    {
        var randomBytes = RandomNumberGenerator.GetBytes(64);

        return Convert.ToBase64String(randomBytes);
    }

    private string GenerateAccessToken(User user)
    {
        var jwtKey = configuration["Jwt:Key"];

        if (string.IsNullOrWhiteSpace(jwtKey))
        {
            throw new InvalidOperationException(
                "JWT Key is missing.");
        }

        var claims = new List<Claim>
    {
        new Claim(
            ClaimTypes.NameIdentifier,
            user.Id.ToString()),

        new Claim(
            ClaimTypes.Name,
            user.Username),

        new Claim(
            ClaimTypes.Role,
            user.Role.Name)
    };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtKey));

        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);

        var expires = DateTime.UtcNow.AddMinutes(
            GetAccessTokenExpiryMinutes());

        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            expires: expires,
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler()
            .WriteToken(token);
    }

    public async Task<AuthResponseDto> RegisterAsync(
        RegisterRequestDto request)
    {
        // Check whether username already exists
        var usernameExists = await dbContext.Users
            .AnyAsync(u => u.Username == request.Username);

        if (usernameExists)
        {
            throw new InvalidOperationException(
                "Username already exists.");
        }

        // Check whether email already exists
        var emailExists = await dbContext.Users
            .AnyAsync(u => u.Email == request.Email);

        if (emailExists)
        {
            throw new InvalidOperationException(
                "Email already exists.");
        }

        // Always assign Viewer role for public registration
        var viewerRole = await dbContext.Roles
            .FirstOrDefaultAsync(r => r.Name == "Viewer");

        if (viewerRole == null)
        {
            throw new InvalidOperationException(
                "Viewer role was not found.");
        }

        // Create user
        var user = new User
        {
            Username = request.Username,
            Email = request.Email,
            RoleId = viewerRole.Id
        };

        // Hash password
        user.PasswordHash = passwordHasher.HashPassword(
            user,
            request.Password);

        // Save user
        await dbContext.Users.AddAsync(user);
        await dbContext.SaveChangesAsync();

        // Registration response
        return new AuthResponseDto
        {
            UserId = user.Id,
            Username = user.Username,
            Role = viewerRole.Name
        };
    }

    public async Task<AuthResponseDto> LoginAsync(
        LoginRequestDto request)
    {
        var user = await dbContext.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u =>
                u.Username == request.Username);

        if (user == null)
        {
            throw new InvalidOperationException(
                "Invalid username or password.");
        }

        var passwordResult = passwordHasher.VerifyHashedPassword(
            user,
            user.PasswordHash,
            request.Password);

        if (passwordResult == PasswordVerificationResult.Failed)
        {
            throw new InvalidOperationException(
                "Invalid username or password.");
        }

        var accessToken = GenerateAccessToken(user);

        var refreshToken = GenerateRefreshToken();

        var refreshTokenEntity = new RefreshToken
        {
            Token = refreshToken,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddDays(
                GetRefreshTokenExpiryDays()),
            UserId = user.Id
        };

        await dbContext.RefreshTokens.AddAsync(refreshTokenEntity);

        await dbContext.SaveChangesAsync();

        return new AuthResponseDto
        {
            UserId = user.Id,
            Username = user.Username,
            Role = user.Role.Name,
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            AccessTokenExpiresAt = DateTime.UtcNow.AddMinutes(
                GetAccessTokenExpiryMinutes())
        };
    }

    public Task<AuthResponseDto> RefreshTokenAsync(
        RefreshTokenRequestDto request)
    {
        throw new NotImplementedException();
    }

    public Task<bool> LogoutAsync(
        string refreshToken)
    {
        throw new NotImplementedException();
    }
}