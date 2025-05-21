// MyBank.WebAPI/Controllers/AuthController.cs
using Microsoft.AspNetCore.Mvc;
using MyBank.Application.Services;
using MyBank.Application.DTOs;
using MyBank.Application.Interfaces;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var token = await _authService.Authenticate(request.Email, request.Password);
        return Ok(new { Token = token });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var result = await _authService.Register(request.Name, request.Email, request.CPF, request.Password);
        return result ? Ok() : BadRequest();
    }
}
namespace MyBank.WebApi.Controllers
{
    public class AuthController
    {
    }
}
