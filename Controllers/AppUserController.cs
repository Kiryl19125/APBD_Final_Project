using FinalProjectAPBD.Context;
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

    [HttpGet("users")]
    public IActionResult GetUsers()
    {
        var result = _context.AppUsers;
        return Ok(result);
    }

    [HttpGet("customers")]
    public IActionResult GetCustomers()
    {
        var result = _context.Customers;
        return Ok(result);
    }
}