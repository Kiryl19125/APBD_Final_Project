using FinalProjectAPBD.Context;
using FinalProjectAPBD.Helpers;
using FinalProjectAPBD.Models;
using FinalProjectAPBD.Services.IncomeService;
using Microsoft.AspNetCore.Authorization;
// using FinalProjectAPBD.Models.ResponceModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FinalProjectAPBD.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IncomeController : ControllerBase
{
    private readonly IIncomeService _service;

    public IncomeController(IIncomeService service)
    {
        _service = service;
    }

    
    [Authorize(Roles = "admin,user")]
    [HttpGet("calculateTotalIncome")]
    public async Task<IActionResult> CalculateCurrentIncome(string currency)
    {
        try
        {
            var income = await _service.CalculateCurrentIncome(currency);
            return Ok($"Total company income is: {income} {currency}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    
    [Authorize(Roles = "admin,user")]
    [HttpGet("calculateIncomeForSoftware")]
    public async Task<IActionResult> CalculateCurrentIncomeForSoftware(int softwareId, string currency)
    {
        try
        {
            var income = await _service.CalculateCurrentIncomeForSoftware(softwareId, currency);
            return Ok($"Total income for software of id: {softwareId} = {income} {currency}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


    
    [Authorize(Roles = "admin,user")]
    [HttpGet("calculateExpectedIncome")]
    public async Task<IActionResult> CalculateExpectedIncome(string currency)
    {
        try
        {
            var income = await _service.CalculateExpectedIncome(currency);
            return Ok($"Expected income is: {income} {currency}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    
    [Authorize(Roles = "admin,user")]
    [HttpGet("calculateExpectedIncomeForSoftware")]
    public async Task<IActionResult> CalculateExpectedIncomeForSoftware(int softwareId, string currency)
    {
        try
        {
            var income = await _service.CalculateExpectedIncomeForSoftware(softwareId, currency);
            return Ok($"Expected income for the software of id {softwareId} is: {income} {currency}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}