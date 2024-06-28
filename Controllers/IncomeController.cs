using FinalProjectAPBD.Context;
using FinalProjectAPBD.Models;
using FinalProjectAPBD.Models.ResponceModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FinalProjectAPBD.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IncomeController : ControllerBase
{
    private BooksContext _context;
    private IConfiguration _configuration;

    public IncomeController(BooksContext context, IConfiguration configuration)
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
        totalIncome = await ConvertCurrency(totalIncome, "PLN", currency);
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
        totalIncome = await ConvertCurrency(totalIncome, "PLN", currency);
        return Ok($"Total income for software of id: {softwareId} = {totalIncome}");
    }


    [HttpGet("calculateExpectedIncome")]
    public async Task<IActionResult> CalculateExpectedIncome(string currency)
    {
        var totalIncomeCustomers = await _context.Contracts.SumAsync(c => c.TotalAmount);
        var totalIncomeCompanies = await _context.ContractsCompanies.SumAsync(c => c.TotalAmount);
        var totalIncome = totalIncomeCustomers + totalIncomeCompanies;
        totalIncome = await ConvertCurrency(totalIncome, "PLN", currency);
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
        totalIncome = await ConvertCurrency(totalIncome, "PLN", currency);

        return Ok($"Expected income for the software of id {softwareId} is: {totalIncome}");
    }


    private async Task<decimal> ConvertCurrency(decimal amount, string fromCurrency, string toCurrency)
    {
        var apiKey = "8126d67dda6f63c7566c7f95";
        var url = $"https://v6.exchangerate-api.com/v6/{apiKey}/latest/{fromCurrency}";

        using (var client = new HttpClient())
        {
            var respone = await client.GetStringAsync(url);
            ExchangeRatesModel exchangeRates = JsonConvert.DeserializeObject<ExchangeRatesModel>(respone);
            exchangeRates.conversion_rates.TryGetValue(toCurrency, out decimal rate);
            return amount * rate;
        }
    }
}