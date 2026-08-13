using Microsoft.AspNetCore.Mvc;
using SmartStore.API.Models.DTO;
using SmartStore.API.Services.Interfaces;

namespace SmartStore.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService authService;

    public AuthController(IAuthService authService)
    {
        this.authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(
        RegisterRequestDto request)
    {
        try
        {
            var result = await authService.RegisterAsync(request);

            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new
            {
                message = ex.Message
            });
        }
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login(
    LoginRequestDto request)
    {
        try
        {
            var result = await authService.LoginAsync(request);

            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return Unauthorized(new
            {
                message = ex.Message
            });
        }
    }
}