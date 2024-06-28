using FinalProjectAPBD.Models;
using FinalProjectAPBD.Models.RequestModels;
using FinalProjectAPBD.Repositories.CompanyRepository;

namespace FinalProjectAPBD.Services.CompanyService;

public class CompanyService : ICompanyService
{
    private readonly ICompanyRepository _repository;

    public CompanyService(ICompanyRepository repository)
    {
        _repository = repository;
    }

    public async Task AddNewCompany(AddNewCompanyModel model)
    {
        
        var newCompany = new Company()
        {
            CompanyName = model.CompanyName,
            Address = model.Address,
            Email = model.Email,
            PhoneNumber = model.PhoneNumber,
            Krs = model.Krs
        };

        await _repository.AddNewCompany(newCompany);
    }

    public async Task UpdateCompanyData(UpdateCompanyModel model)
    {
        await _repository.UpdateCompanyData(model);
    }

    public async Task CreateContractCompany(CreateCompanyContractModel model)
    {
        await _repository.CreateContractCompany(model);
    }

    public async Task ProcessPaymentCompany(ProcessPaymentCompanyRequestModel model)
    {
        await _repository.ProcessPaymentCompany(model);
    }
}