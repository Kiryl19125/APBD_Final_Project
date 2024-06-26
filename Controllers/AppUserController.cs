using FinalProjectAPBD.Context;
using FinalProjectAPBD.Helpers;
using FinalProjectAPBD.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectAPBD.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AppUserController : ControllerBase
{
    private BooksContext _context;

    public AppUserController(BooksContext context)
    {
        _context = context;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public IActionResult RegisterUser(RegisterRequest model)
    {
        var hashedPasswordAndSalt = SecurityHelpers.GetHashedPasswordAndSalt(model.Password);

        var user = new AppUser()
        {
            Email = model.Email,
            Login = model.Login,
            Password = hashedPasswordAndSalt.Item1,
            Salt = hashedPasswordAndSalt.Item2,
            RefreshToken = SecurityHelpers.GenerateRefreshToken(),
            RefreshTockenExp = DateTime.Now.AddDays(1)
        };

        _context.AppUsers.Add(user);
        _context.SaveChanges();

        return Ok();
    }
}