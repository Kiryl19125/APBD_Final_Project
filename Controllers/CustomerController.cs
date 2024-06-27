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

    [Authorize(Roles = "user,admin")]
    [HttpPost("createNewContract")]
    public async Task<IActionResult> CreateContract(CreateCustomerContractModel model)
    {
        // check if customer already has this software
        // get customers software list
        var softwareList = _context.Customers
            .Where(c => c.CustomerId == model.CustomerID)
            .SelectMany(c => c.Contracts)
            .Select(c => c.Software)
            .ToList();
        if (softwareList.Any(s => s.SoftwareId == model.SoftwareID))
        {
            return BadRequest("Customer already has this software"); // TODO! throw custom exception
        }


        // check contract time period       
        if ((model.EndDate - DateTime.Now).TotalDays <= 3 ||
            (model.EndDate - DateTime.Now).TotalDays >= 30)
        {
            return BadRequest("Incorrect date period"); // TODO! throw custom exception
        }

        // calculate total price

        // var software = await _context.Softwares.FindAsync(model.SoftwareID);
        // if (software == null)
        //     throw new Exception("Software not fount"); // TODO! make custom exeption

        var discount = await _context.Discounts.FindAsync(model.DiscountID);
        if (discount == null)
            throw new Exception("Discount not fount"); // TODO! make custom exeption
        decimal discountedPrice = model.Price - (model.Price * (discount.DiscountPercentage / 100));


        var newContract = new Contract()
        {
            CustomerId = model.CustomerID,
            SoftwareId = model.SoftwareID,
            StartDate = model.StratDate,
            EndDate = model.EndDate,
            TotalAmount = discountedPrice,
            DiscountId = model.DiscountID,
            IsSigned = false
        };

        await _context.Contracts.AddAsync(newContract);
        await _context.SaveChangesAsync();
        return Ok("New contract was created");
    }

    [HttpGet("showCustomersSoftware/{customerId:int}")]
    public IActionResult ShowCustomerSoftware(int customerId)
    {
        var softwareList = _context.Customers
            .Where(c => c.CustomerId == customerId)
            .SelectMany(c => c.Contracts)
            .Select(c => c.Software)
            .ToList();

        return Ok(softwareList);
    }
}