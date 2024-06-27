using FinalProjectAPBD.Context;
using FinalProjectAPBD.Models;
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

    [Authorize(Roles = "admin")]
    [HttpDelete("deleteCustomer")]
    public async Task<IActionResult> DeleteCustomer(DeleteCustomerModel model)
    {
        var customer = await _context.Customers.SingleOrDefaultAsync(c => c.Pesel == model.PESEL);
        if (customer == null) return NotFound($"customer with PESEL: {model.PESEL} not fount");
        customer.IsDeleted = true;
        await _context.SaveChangesAsync();
        return Ok("customer deleted successfully.");

    }

    [Authorize(Roles = "admin")]
    [HttpPut("updateCustomerData")]
    public async Task<IActionResult> UpdateCustomerData(UpdateCustomerModel model)
    {
        var customer = await _context.Customers.SingleOrDefaultAsync(c => c.Pesel == model.PESEL);
        if (customer == null) return NotFound($"customer with PESEL: {model.PESEL} not fount");
        if (customer.IsDeleted)
        {
            return BadRequest($"customer with PESEL: {model.PESEL} already deleted");
        }

        customer.FirstName = model.FirstName;
        customer.LastName = model.LastName;
        customer.Address = model.Address;
        customer.Email = model.Email;
        customer.PhoneNumber = model.PhoneNumber;
        await _context.SaveChangesAsync();

        return Ok($"customer with PESEL: {model.PESEL} updated successfully");

    }

    [Authorize(Roles = "admin,user")]
    [HttpPost("addNewCustomer")]
    public async Task<IActionResult> AddNewCustomer(AddNewCustomerModel model)
    {
        var newCustomer = new Customer()
        {
            Pesel = model.Pesel,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Address = model.Address,
            Email = model.Email,
            PhoneNumber = model.PhoneNumber,
            IsDeleted = false
        };

        await _context.Customers.AddAsync(newCustomer);
        await _context.SaveChangesAsync();

        return Ok("new customer was added successfully.");

    }
}