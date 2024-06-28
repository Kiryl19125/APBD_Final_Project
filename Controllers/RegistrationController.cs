using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FinalProjectAPBD.Context;
using FinalProjectAPBD.Helpers;
using FinalProjectAPBD.Models;
using FinalProjectAPBD.Models.ResponseModels;
using FinalProjectAPBD.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using JwtSecurityToken = System.IdentityModel.Tokens.Jwt.JwtSecurityToken;

namespace FinalProjectAPBD.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RegistrationController : ControllerBase
{
    // private DBContext _context;
    private IConfiguration _configuration;
    public readonly IRegistrationService _service;

    public RegistrationController(IRegistrationService service, IConfiguration configuration)
    {
        _configuration = configuration;
        _service = service;
    }

    // [HttpGet("getUsers")]
    // public IActionResult GetAppUsers()
    // {
    //     var result = _context.AppUsers;
    //     return Ok(result);
    // }
    
    
    // [Authorize(Roles = "admin")]
    // [Authorize]
    // [HttpGet("SecretData")]
    // public IActionResult GetSecretData()
    // {
    //     var roles = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
    //     if (roles.Any())
    //     {
    //         return Ok(roles);
    //     }
    //
    //     return Unauthorized();
    // }

    // [AllowAnonymous]
    // [HttpGet("GetPublicData")]
    // public IActionResult GetPublicData()
    // {
    //     return Ok("Public Data");
    // }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser(RegisterRequest model)
    {
        await _service.RegisterUser(model);
        return Ok();
    }


    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest loginRequest)
    {
        var responce = await _service.Login(loginRequest);
        return Ok(responce);
    }

    [Authorize(AuthenticationSchemes = "IgnoreTokenExpirationScheme")]
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(RefreshTokenRequest refreshToken)
    {
        var response = _service.Refresh(refreshToken);
        return Ok(response);
    }
}