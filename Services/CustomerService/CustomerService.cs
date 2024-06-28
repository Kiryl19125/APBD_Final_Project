using FinalProjectAPBD.Models;
using FinalProjectAPBD.Models.RequestModels;
using FinalProjectAPBD.Repositories.CustomerRepository;

namespace FinalProjectAPBD.Services.CustomerService;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _repository;

    public CustomerService(ICustomerRepository repository)
    {
        _repository = repository;
    }

    public async Task DeleteCustomer(DeleteCustomerModel model)
    {
        await _repository.DeleteCustomer(model);
    }

    public async Task UpdateCustomerData(UpdateCustomerModel model)
    {
        await _repository.UpdateCustomerData(model);
    }

    public async Task AddNewCustomer(AddNewCustomerModel model)
    {
        
        var newCustomer = new Customer()
        {
            Pesel = model.Pesel,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Address = model.Address,
            Email = model.Email,
            PhoneNumber = model.PhoneNumber,
            IsDeleted = false
        };
        await _repository.AddNewCustomer(newCustomer);
    }

    public async Task CreateContract(CreateCustomerContractModel model)
    {
        await _repository.CreateContract(model);
    }

    public async Task ProcessPayment(ProcessPaymentRequestModel model)
    {
        await _repository.ProcessPayment(model);
    }
}