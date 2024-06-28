using System.Runtime.CompilerServices;
using FinalProjectAPBD.Context;
using FinalProjectAPBD.Models;
using FinalProjectAPBD.Models.RequestModels;
using FinalProjectAPBD.Services.CustomerService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectAPBD.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _service;

    public CustomerController(ICustomerService service)
    {
        _service = service;
    }

    // [Authorize(Roles = "admin")]
    [HttpDelete("deleteCustomer")]
    public async Task<IActionResult> DeleteCustomer(DeleteCustomerModel model)
    {
        try
        {
            await _service.DeleteCustomer(model);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        return Ok("customer deleted successfully.");
    }

    // [Authorize(Roles = "admin")]
    [HttpPut("updateCustomerData")]
    public async Task<IActionResult> UpdateCustomerData(UpdateCustomerModel model)
    {
        try
        {
            await _service.UpdateCustomerData(model);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        return Ok($"customer with PESEL: {model.PESEL} updated successfully");
    }

    // [Authorize(Roles = "admin,user")]
    [HttpPost("addNewCustomer")]
    public async Task<IActionResult> AddNewCustomer(AddNewCustomerModel model)
    {
        try
        {
            await _service.AddNewCustomer(model);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        return Ok("new customer was added successfully.");
    }

    // [Authorize(Roles = "user,admin")]
    [HttpPost("createNewContract")]
    public async Task<IActionResult> CreateContract(CreateCustomerContractModel model)
    {
        try
        {
            await _service.CreateContract(model);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        return Ok("New contract was created");
    }

    [HttpPost("makePayments")]
    public async Task<IActionResult> ProcessPayment(ProcessPaymentRequestModel model)
    {
        try
        {
            await _service.ProcessPayment(model);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        return Ok("Payment was created successfully");
    }
}