using SmartStore.API.Models.DTO;

namespace SmartStore.API.Services.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDto> RegisterAsync(
        RegisterRequestDto request);

    Task<AuthResponseDto> LoginAsync(
        LoginRequestDto request);

    Task<AuthResponseDto> RefreshTokenAsync(
        RefreshTokenRequestDto request);

    Task<bool> LogoutAsync(
        string refreshToken);
}