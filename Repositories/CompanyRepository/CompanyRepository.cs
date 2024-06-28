using FinalProjectAPBD.Context;
using FinalProjectAPBD.Exceptions;
using FinalProjectAPBD.Models;
using FinalProjectAPBD.Models.RequestModels;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectAPBD.Repositories.CompanyRepository;

public class CompanyRepository : ICompanyRepository
{

    private readonly DBContext _context;

    public CompanyRepository(DBContext context)
    {
        _context = context;
    }

    public async Task AddNewCompany(Company company)
    {
        await _context.Companies.AddAsync(company);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCompanyData(UpdateCompanyModel model)
    {
        var company = await _context.Companies.SingleOrDefaultAsync(c => c.Krs == model.Krs);
        if (company == null)
        {
            // return NotFound($"company with KRS: {model.Krs} not fount");
            // throw new Exception($"company with KRS: {model.Krs} not fount");
            throw new CompanyNotFound($"company with KRS: {model.Krs} not fount");
        }
        company.CompanyName = model.CompanyName;
        company.Address = model.Address;
        company.Email = model.Email;
        company.PhoneNumber = model.PhoneNumber;

        await _context.SaveChangesAsync();
    }

    public async Task CreateContractCompany(CreateCompanyContractModel model)
    {
        // check if customer already has this software
        // get customers software list
        var softwareList = _context.Companies
            .Where(c => c.CompanyId == model.CompanyID)
            .SelectMany(c => c.ContractsCompanies)
            .Select(c => c.Software)
            .ToList();
        if (softwareList.Any(s => s.SoftwareId == model.SoftwareID))
        {
            // return BadRequest("Customer already has this software"); // TODO! throw custom exception
            // throw new Exception("Customer already has this software");
            throw new DuplicatedContract("Customer already has this software");
        }


        // check contract time period       
        if ((model.EndDate - DateTime.Now).TotalDays <= 3 ||
            (model.EndDate - DateTime.Now).TotalDays >= 30)
        {
            // return BadRequest("Incorrect date period"); // TODO! throw custom exception
            // throw new Exception("Incorrect date period");
            throw new InvalidTimePeriod("Incorrect date period");
        }

        // calculate total price
        var discount = await _context.Discounts.FindAsync(model.DiscountID);
        if (discount == null)
        {
            // throw new Exception("Discount not fount"); // TODO! make custom exeption
            throw new DiscountNotFound("Discount not fount");
        }
        decimal discountedPrice = model.Price - (model.Price * (discount.DiscountPercentage / 100));


        var newContract = new ContractsCompany()
        {
            CompanyId = model.CompanyID,
            SoftwareId = model.SoftwareID,
            StartDate = model.StratDate,
            EndDate = model.EndDate,
            TotalAmount = discountedPrice,
            DiscountId = model.DiscountID,
            IsSigned = false,
            IsActive = model.IsActive
        };

        await _context.ContractsCompanies.AddAsync(newContract);
        await _context.SaveChangesAsync();
    }

    public async Task ProcessPaymentCompany(ProcessPaymentCompanyRequestModel model)
    {
        var contract = await _context.ContractsCompanies.FindAsync(model.ContractID);
        if (contract == null)
        {
            throw new ContractDoesNotExists($"contract of id: {model.ContractID} does not exist");
        }
            // throw new Exception($"contract of id: {model.ContractID} does not exist");
            // return BadRequest($"contract of id: {model.ContractID} does not exist");

        if (DateTime.Now > contract.EndDate)
        {
            throw new ExpiredDate("Contract end date is expired");
            // return BadRequest("Contract end date is expired");
        }

        var customer = await _context.Companies.FindAsync(model.CompanyID);
        if (customer == null)
            throw new CustomerDoesNotExists($"customer of id: {model.CompanyID} does not exist");
            // return BadRequest($"customer of id: {model.CompanyID} does not exist");

            if (contract.Payed + model.Amount <= contract.TotalAmount)
                contract.Payed += model.Amount;
            else
                throw new PaymentOverflow($"Amount: {model.Amount} overflow");
            // return BadRequest($"Amount: {model.Amount} overflow");

        if (contract.Payed == contract.TotalAmount)
        {
            contract.IsActive = false;
            // return Ok("Contract is fully payed");
        }


        var newPayment = new PaymentsCompany()
        {
            ContractId = model.ContractID,
            PaymentDate = model.PaymentDate,
            Amount = model.Amount
        };

        await _context.PaymentsCompanies.AddAsync(newPayment);
        await _context.SaveChangesAsync();
    }
}