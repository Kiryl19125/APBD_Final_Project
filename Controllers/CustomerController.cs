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
        await _service.DeleteCustomer(model);

        return Ok("customer deleted successfully.");
    }

    // [Authorize(Roles = "admin")]
    [HttpPut("updateCustomerData")]
    public async Task<IActionResult> UpdateCustomerData(UpdateCustomerModel model)
    {
        await _service.UpdateCustomerData(model);

        return Ok($"customer with PESEL: {model.PESEL} updated successfully");
    }

    // [Authorize(Roles = "admin,user")]
    [HttpPost("addNewCustomer")]
    public async Task<IActionResult> AddNewCustomer(AddNewCustomerModel model)
    {
        await _service.AddNewCustomer(model);

        return Ok("new customer was added successfully.");
    }

    // [Authorize(Roles = "user,admin")]
    [HttpPost("createNewContract")]
    public async Task<IActionResult> CreateContract(CreateCustomerContractModel model)
    {
        await _service.CreateContract(model);
        return Ok("New contract was created");
    }

    [HttpPost("makePayments")]
    public async Task<IActionResult> ProcessPayment(ProcessPaymentRequestModel model)
    {
        await _service.ProcessPayment(model);
        return Ok("Payment was created successfully");
    }
}