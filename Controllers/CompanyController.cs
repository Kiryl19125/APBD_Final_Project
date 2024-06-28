using FinalProjectAPBD.Context;
using FinalProjectAPBD.Models;
using FinalProjectAPBD.Models.RequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectAPBD.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CompanyController : ControllerBase
{

    private DBContext _context;

    public CompanyController(DBContext context)
    {
        _context = context;
    }

    // [Authorize(Roles = "admin,user")]
    [HttpPost("addNewCompany")]
    public async Task<IActionResult> AddNewCompany(AddNewCompanyModel model)
    {
        var newCompany = new Company()
        {
            CompanyName = model.CompanyName,
            Address = model.Address,
            Email = model.Email,
            PhoneNumber = model.PhoneNumber,
            Krs = model.Krs
        };

        await _context.Companies.AddAsync(newCompany);
        await _context.SaveChangesAsync();
        
        return Ok("new company was added successfully.");
 
    }

    
    // [Authorize(Roles = "admin")]
    [HttpPut("updateCompanyData")]
    public async Task<IActionResult> UpdateCompanyData(UpdateCompanyModel model)
    {
        var company = await _context.Companies.SingleOrDefaultAsync(c => c.Krs == model.Krs);
        if (company == null) return NotFound($"company with KRS: {model.Krs} not fount");
        company.CompanyName = model.CompanyName;
        company.Address = model.Address;
        company.Email = model.Email;
        company.PhoneNumber = model.PhoneNumber;

        await _context.SaveChangesAsync();
            
        return Ok($"company with KRS: {model.Krs} updated successfully");

    }

    [HttpPost("createNewContractCompany")]
    public async Task<IActionResult> CreateContractCompany(CreateCompanyContractModel model)
    {
        
        // check if customer already has this software
        // get customers software list
        var softwareList = _context.Customers
            .Where(c => c.CustomerId == model.CompanyID)
            .SelectMany(c => c.Contracts)
            .Select(c => c.Software)
            .ToList();
        if (softwareList.Any(s => s.SoftwareId == model.SoftwareID))
        {
            return BadRequest("Customer already has this software"); // TODO! throw custom exception
        }


        // check contract time period       
        if ((model.EndDate - DateTime.Now).TotalDays <= 3 ||
            (model.EndDate - DateTime.Now).TotalDays >= 30)
        {
            return BadRequest("Incorrect date period"); // TODO! throw custom exception
        }

        // calculate total price

        // var software = await _context.Softwares.FindAsync(model.SoftwareID);
        // if (software == null)
        //     throw new Exception("Software not fount"); // TODO! make custom exeption

        var discount = await _context.Discounts.FindAsync(model.DiscountID);
        if (discount == null)
            throw new Exception("Discount not fount"); // TODO! make custom exeption
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
        return Ok("New contract with company was created");
    }

    [HttpPost("makePaymentCompany")]
    public async Task<IActionResult> ProcessPaymentCompany(ProcessPaymentCompanyRequestModel model)
    {
        
        var contract = await _context.ContractsCompanies.FindAsync(model.ContractID);
        if (contract == null)
            return BadRequest($"contract of id: {model.ContractID} does not exist");

        if (DateTime.Now > contract.EndDate)
        {
            return BadRequest("Contract end date is expired");
        }
        
        var customer = await _context.Companies.FindAsync(model.CompanyID);
        if (customer == null)
            return BadRequest($"customer of id: {model.CompanyID} does not exist");

        if (contract.Payed + model.Amount <= contract.TotalAmount)
            contract.Payed += model.Amount;
        else
            return BadRequest($"Amount: {model.Amount} overflow");

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

        return Ok("Payment was created successfully");
    }

    [HttpGet("showCompaniesSoftware")]
    public IActionResult ShowCompaniesSoftware(int companyId)
    {
        var softwareList = _context.Companies
            .Where(c => c.CompanyId == companyId)
            .SelectMany(c => c.ContractsCompanies)
            .Select(c => c.Software)
            .ToList();
        return Ok(softwareList);
    }
}