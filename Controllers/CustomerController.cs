using FinalProjectAPBD.Context;
using FinalProjectAPBD.Models.RequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectAPBD.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private BooksContext _context;

    public CustomerController(BooksContext context)
    {
        _context = context;
    }

    // [Authorize(Roles = "admin")]
    [HttpDelete("deleteCustomer")]
    public async Task<IActionResult> DeleteCustomer(DeleteCustomerModel model)
    {
        var customer = await _context.Customers.SingleOrDefaultAsync(c => c.Pesel == model.PESEL);
        if (customer != null)
        {
            customer.IsDeleted = true;
            await _context.SaveChangesAsync();
            return Ok("customer deleted successfully.");
        }

        return NotFound($"customer with PESEL: {model.PESEL} not fount");
    }

    // [Authorize(Roles = "admin")]
    // [HttpPost("updateCustomerData")]
    // public IActionResult UpdateCustomerData()
    // {
    //     
    // }
}