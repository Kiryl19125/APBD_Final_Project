using FinalProjectAPBD.Models.RequestModels;

namespace FinalProjectAPBD.Services.CompanyService;

public interface ICompanyService
{
    
    public Task AddNewCompany(AddNewCompanyModel model);
    public Task UpdateCompanyData(UpdateCompanyModel model);
    public Task CreateContractCompany(CreateCompanyContractModel model);
    public Task ProcessPaymentCompany(ProcessPaymentCompanyRequestModel model);
}