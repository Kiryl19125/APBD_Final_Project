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

    private BooksContext _context;

    public CompanyController(BooksContext context)
    {
        _context = context;
    }

    [Authorize(Roles = "admin,user")]
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

    
    [Authorize(Roles = "admin")]
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
}