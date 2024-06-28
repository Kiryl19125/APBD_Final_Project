using FinalProjectAPBD.Models.RequestModels;

namespace FinalProjectAPBD.Repositories.CustomerRepository;

public interface ICustomerRepository
{
    public Task DeleteCustomer(DeleteCustomerModel model);
    public Task UpdateCustomerData(UpdateCustomerModel model);
    public Task AddNewCustomer(AddNewCustomerModel model);
    public Task CreateContract(CreateCustomerContractModel model);
    public Task ProcessPayment(ProcessPaymentRequestModel model);
}