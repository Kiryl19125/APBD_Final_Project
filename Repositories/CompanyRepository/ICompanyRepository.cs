using FinalProjectAPBD.Models;
using FinalProjectAPBD.Models.RequestModels;

namespace FinalProjectAPBD.Repositories.CompanyRepository;

public interface ICompanyRepository
{
    public Task AddNewCompany(Company company);
    public Task UpdateCompanyData(UpdateCompanyModel model);
    
    public Task CreateContractCompany(CreateCompanyContractModel model);
    
    public Task ProcessPaymentCompany(ProcessPaymentCompanyRequestModel model);
}