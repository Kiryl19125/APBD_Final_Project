using FinalProjectAPBD.Context;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectAPBD.Repositories.IncomeRepository;

public class IncomeRepository : IIncomeRepository
{
    private readonly DBContext _context;

    public IncomeRepository(DBContext context)
    {
        _context = context;
    }

    public async Task<decimal> CalculateCurrentIncome()
    {
        
        var totalIncomeCustomers = await _context.Payments.SumAsync(p => p.Amount);
        var totalIncomeCompanies = await _context.PaymentsCompanies.SumAsync(p => p.Amount);
        var totalIncome = totalIncomeCustomers + totalIncomeCompanies;
        return totalIncome;
    }

    public async Task<decimal> CalculateCurrentIncomeForSoftware(int softwareId)
    {
        var totalIncomeCustomers =
            await _context.Payments.Where(p => p.Contract.SoftwareId == softwareId).SumAsync(p => p.Amount);
        var totalIncomeCompanies =
            await _context.PaymentsCompanies.Where(p => p.Contract.SoftwareId == softwareId).SumAsync(p => p.Amount);
        var totalIncome = totalIncomeCustomers + totalIncomeCompanies;

        return totalIncome;
    }

    public async Task<decimal> CalculateExpectedIncome()
    {
        var totalIncomeCustomers = await _context.Contracts.SumAsync(c => c.TotalAmount);
        var totalIncomeCompanies = await _context.ContractsCompanies.SumAsync(c => c.TotalAmount);
        var totalIncome = totalIncomeCustomers + totalIncomeCompanies;

        return totalIncome;
    }

    public async Task<decimal> CalculateExpectedIncomeForSoftware(int softwareId)
    {
        var totalIncomeCustomers = await _context.Contracts.Where(c => c.SoftwareId == softwareId)
            .SumAsync(c => c.TotalAmount);
        var totalIncomeCompanies = await _context.ContractsCompanies.Where(c => c.SoftwareId == softwareId)
            .SumAsync(c => c.TotalAmount);
        var totalIncome = totalIncomeCustomers + totalIncomeCompanies;

        return totalIncome;
    }
}