namespace FinalProjectAPBD.Repositories.IncomeRepository;

public interface IIncomeRepository
{
    public Task<decimal> CalculateCurrentIncome();
    
    public Task<decimal> CalculateCurrentIncomeForSoftware(int softwareId);
    
    public Task<decimal> CalculateExpectedIncome();
    
    public Task<decimal> CalculateExpectedIncomeForSoftware(int softwareId);
}