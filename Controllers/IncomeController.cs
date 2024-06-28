using FinalProjectAPBD.Context;
using FinalProjectAPBD.Helpers;
using FinalProjectAPBD.Models;
// using FinalProjectAPBD.Models.ResponceModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FinalProjectAPBD.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IncomeController : ControllerBase
{
    private DBContext _context;
    private IConfiguration _configuration;

    public IncomeController(DBContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    [HttpGet("calculateTotalIncome")]
    public async Task<IActionResult> CalculateCurrentIncome(string currency)
    {
        var totalIncomeCustomers = await _context.Payments.SumAsync(p => p.Amount);
        var totalIncomeCompanies = await _context.PaymentsCompanies.SumAsync(p => p.Amount);
        var totalIncome = totalIncomeCustomers + totalIncomeCompanies;
        totalIncome = await CurrencyHelper.ConvertCurrency(totalIncome, "PLN", currency);
        return Ok($"Total company income is: {totalIncome} {currency}");
    }

    [HttpGet("calculateIncomeForSoftware")]
    public async Task<IActionResult> CalculateCurrentIncomeForSoftware(int softwareId, string currency)
    {
        var totalIncomeCustomers =
            await _context.Payments.Where(p => p.Contract.SoftwareId == softwareId).SumAsync(p => p.Amount);
        var totalIncomeCompanies =
            await _context.PaymentsCompanies.Where(p => p.Contract.SoftwareId == softwareId).SumAsync(p => p.Amount);
        var totalIncome = totalIncomeCustomers + totalIncomeCompanies;
        totalIncome = await CurrencyHelper.ConvertCurrency(totalIncome, "PLN", currency);
        return Ok($"Total income for software of id: {softwareId} = {totalIncome}");
    }


    [HttpGet("calculateExpectedIncome")]
    public async Task<IActionResult> CalculateExpectedIncome(string currency)
    {
        var totalIncomeCustomers = await _context.Contracts.SumAsync(c => c.TotalAmount);
        var totalIncomeCompanies = await _context.ContractsCompanies.SumAsync(c => c.TotalAmount);
        var totalIncome = totalIncomeCustomers + totalIncomeCompanies;
        totalIncome = await CurrencyHelper.ConvertCurrency(totalIncome, "PLN", currency);
        return Ok($"Expected income is: {totalIncome} {currency}");
    }

    [HttpGet("calculateExpectedIncomeForSoftware")]
    public async Task<IActionResult> CalculateExpectedIncomeForSoftware(int softwareId, string currency)
    {
        var totalIncomeCustomers = await _context.Contracts.Where(c => c.SoftwareId == softwareId)
            .SumAsync(c => c.TotalAmount);
        var totalIncomeCompanies = await _context.ContractsCompanies.Where(c => c.SoftwareId == softwareId)
            .SumAsync(c => c.TotalAmount);
        var totalIncome = totalIncomeCustomers + totalIncomeCompanies;
        totalIncome = await CurrencyHelper.ConvertCurrency(totalIncome, "PLN", currency);

        return Ok($"Expected income for the software of id {softwareId} is: {totalIncome}");
    }
}