using FinalProjectAPBD.Helpers;
using FinalProjectAPBD.Repositories.IncomeRepository;

namespace FinalProjectAPBD.Services.IncomeService;

public class IncomeService : IIncomeService
{
    private readonly IIncomeRepository _repository;

    public IncomeService(IIncomeRepository repository)
    {
        _repository = repository;
    }

    public async Task<decimal> CalculateCurrentIncome(string currency)
    {
        var income = await _repository.CalculateCurrentIncome();
        var convertedIncome = await CurrencyHelper.ConvertCurrency(income, "PLN", currency);
        return convertedIncome;
    }

    public async Task<decimal> CalculateCurrentIncomeForSoftware(int softwareId, string currency)
    {
        var income = await _repository.CalculateCurrentIncomeForSoftware(softwareId);
        var convertedIncome = await CurrencyHelper.ConvertCurrency(income, "PLN", currency);
        return convertedIncome;
    }

    public async Task<decimal> CalculateExpectedIncome(string currency)
    {
        var income = await _repository.CalculateExpectedIncome();
        var convertedIncome = await CurrencyHelper.ConvertCurrency(income, "PLN", currency);
        return convertedIncome;
    }

    public async Task<decimal> CalculateExpectedIncomeForSoftware(int softwareId, string currency)
    {
        var income = await _repository.CalculateExpectedIncomeForSoftware(softwareId);
        var convertedIncome = await CurrencyHelper.ConvertCurrency(income, "PLN", currency);
        return convertedIncome;
    }
}