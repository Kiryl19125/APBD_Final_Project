using FinalProjectAPBD.Context;
using FinalProjectAPBD.Models;
using FinalProjectAPBD.Models.RequestModels;
using FinalProjectAPBD.Services.CompanyService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectAPBD.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CompanyController : ControllerBase
{
    private readonly ICompanyService _service;

    public CompanyController(ICompanyService service)
    {
        _service = service;
    }

    [Authorize(Roles = "admin,user")]
    [HttpPost("addNewCompany")]
    public async Task<IActionResult> AddNewCompany(AddNewCompanyModel model)
    {
        try
        {
            await _service.AddNewCompany(model);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        return Ok("new company was added successfully.");
    }


    [Authorize(Roles = "admin")]
    [HttpPut("updateCompanyData")]
    public async Task<IActionResult> UpdateCompanyData(UpdateCompanyModel model)
    {
        try
        {
            await _service.UpdateCompanyData(model);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        return Ok($"company with KRS: {model.Krs} updated successfully");
    }

    [Authorize(Roles = "admin,user")]
    [HttpPost("createNewContractCompany")]
    public async Task<IActionResult> CreateContractCompany(CreateCompanyContractModel model)
    {
        try
        {
            await _service.CreateContractCompany(model);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        return Ok("New contract with company was created");
    }

    
    [Authorize(Roles = "admin,user")]
    [HttpPost("makePaymentCompany")]
    public async Task<IActionResult> ProcessPaymentCompany(ProcessPaymentCompanyRequestModel model)
    {
        try
        {
            await _service.ProcessPaymentCompany(model);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        return Ok("Payment was created successfully");
    }
}