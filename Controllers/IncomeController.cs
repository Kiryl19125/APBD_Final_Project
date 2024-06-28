using FinalProjectAPBD.Context;
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
        var totalIncome = await _context.Payments.SumAsync(p => p.Amount);
        // var convertedCurrency = await ConvertCurrency(totalIncome, "PLN", currency);
        return Ok($"Total company income is: {totalIncome} {currency}");
    }

    [HttpGet("calculateIncomeForSoftware")]
    public async Task<IActionResult> CalculateCurrentIncomeForSoftware(int softwareId)
    {
        var totalIncome =
            await _context.Payments.Where(p => p.Contract.SoftwareId == softwareId).SumAsync(p => p.Amount);
        return Ok($"Total income for software of id: {softwareId} = {totalIncome}");
    }


    [HttpGet("calculateExpectedIncome")]
    public async Task<IActionResult> CalculateExpectedIncome(string currency)
    {
        var totalIncome = await _context.Contracts.SumAsync(c => c.TotalAmount);
        return Ok($"Expected income is: {totalIncome} {currency}");
    }

    [HttpGet("calculateExpectedIncomeForSoftware")]
    public async Task<IActionResult> CalculateExpectedIncomeForSoftware(int softwareId)
    {
        var totalIncome = await _context.Contracts.Where(c => c.SoftwareId == softwareId)
            .SumAsync(c => c.TotalAmount);

        return Ok($"Expected income for the software of id {softwareId} is: {totalIncome}");
    }


    // public IActionResult CalculateExpectedIncome()
    // {
    //     
    // }

    // TODO! FIX
    private async Task<decimal> ConvertCurrency(decimal amount, string fromCurrency, string toCurrency)
    {
        using (var client = new HttpClient())
        {
            var apiKey = "8df5647f999907ba2eb3be42fed8d7a5";
            var url =
                $"https://api.exchangeratesapi.io/latest?base={fromCurrency}&symbols={toCurrency}&access_key={apiKey}";
            var response = await client.GetStringAsync(url);
            var rates = JsonConvert.DeserializeObject<CurrencyResponseModel>(response);

            if (rates.Rates.TryGetValue(toCurrency.ToUpper(), out var rate))
            {
                return amount * rate;
            }
            else
            {
                throw new Exception("Invalid currency or conversion rate not found.");
            }
        }
    }
}