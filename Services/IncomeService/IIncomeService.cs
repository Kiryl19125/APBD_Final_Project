namespace FinalProjectAPBD.Services.IncomeService;

public interface IIncomeService
{
    public Task<decimal> CalculateCurrentIncome(string currency);

    public Task<decimal> CalculateCurrentIncomeForSoftware(int softwareId, string currency);

    public Task<decimal> CalculateExpectedIncome(string currency);

    public Task<decimal> CalculateExpectedIncomeForSoftware(int softwareId, string currency);
}