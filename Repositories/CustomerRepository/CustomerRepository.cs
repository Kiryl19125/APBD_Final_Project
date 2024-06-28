using FinalProjectAPBD.Context;
using FinalProjectAPBD.Exceptions;
using FinalProjectAPBD.Models;
using FinalProjectAPBD.Models.RequestModels;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectAPBD.Repositories.CustomerRepository;

public class CustomerRepository : ICustomerRepository
{
    private readonly DBContext _context;

    public CustomerRepository(DBContext context)
    {
        _context = context;
    }

    public async Task DeleteCustomer(DeleteCustomerModel model)
    {
        var customer = await _context.Customers.SingleOrDefaultAsync(c => c.Pesel == model.PESEL);
        if (customer == null)
        {
            throw new CustomerDoesNotExists($"customer with PESEL: {model.PESEL} not found");
        }

        customer.IsDeleted = true;
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCustomerData(UpdateCustomerModel model)
    {
        var customer = await _context.Customers.SingleOrDefaultAsync(c => c.Pesel == model.PESEL);
        if (customer == null) throw new Exception($"customer with PESEL: {model.PESEL} not found");
        if (customer.IsDeleted)
        {
            throw new CustomerDoesNotExists($"Customer with PESEL: {model.PESEL} deleted");
        }

        customer.FirstName = model.FirstName;
        customer.LastName = model.LastName;
        customer.Address = model.Address;
        customer.Email = model.Email;
        customer.PhoneNumber = model.PhoneNumber;
        await _context.SaveChangesAsync();
    }

    public async Task AddNewCustomer(Customer customer)
    {
        await _context.Customers.AddAsync(customer);
        await _context.SaveChangesAsync();
    }

    public async Task CreateContract(CreateCustomerContractModel model)
    {
        var softwareList = _context.Customers
            .Where(c => c.CustomerId == model.CustomerID)
            .SelectMany(c => c.Contracts)
            .Select(c => c.Software)
            .ToList();
        if (softwareList.Any(s => s.SoftwareId == model.SoftwareID))
        {
            throw new DuplicatedContract("Customer already has this software");
        }


        if ((model.EndDate - DateTime.Now).TotalDays <= 3 ||
            (model.EndDate - DateTime.Now).TotalDays >= 30)
        {
            throw new InvalidTimePeriod("Incorrect date period");
        }


        var discount = await _context.Discounts.FindAsync(model.DiscountID);
        if (discount == null)
        {
            throw new DiscountNotFound("Discount not fount"); // TODO! make custom exeption
        }

        decimal discountedPrice = model.Price - (model.Price * (discount.DiscountPercentage / 100));


        var newContract = new Contract()
        {
            CustomerId = model.CustomerID,
            SoftwareId = model.SoftwareID,
            StartDate = model.StratDate,
            EndDate = model.EndDate,
            TotalAmount = discountedPrice,
            DiscountId = model.DiscountID,
            IsSigned = false,
            IsActive = model.IsActive
        };

        await _context.Contracts.AddAsync(newContract);
        await _context.SaveChangesAsync();
    }

    public async Task ProcessPayment(ProcessPaymentRequestModel model)
    {
        var contract = await _context.Contracts.FindAsync(model.ContractID);
        if (contract == null)
            throw new ContractDoesNotExists($"contract of id: {model.ContractID} does not exist");

        if (DateTime.Now > contract.EndDate)
        {
            throw new InvalidTimePeriod("Contract end date is expired");
        }

        var customer = await _context.Customers.FindAsync(model.CustomerID);
        if (customer == null)
            throw new CustomerDoesNotExists($"customer of id: {model.CustomerID} does not exist");

        if (contract.Payed + model.Amount <= contract.TotalAmount)
            contract.Payed += model.Amount;
        else
            throw new PaymentOverflow($"Amount: {model.Amount} overflow");

        if (contract.Payed == contract.TotalAmount)
        {
            contract.IsActive = false;
        }


        var newPayment = new Payment()
        {
            ContractId = model.ContractID,
            PaymentDate = model.PaymentDate,
            Amount = model.Amount
        };

        await _context.Payments.AddAsync(newPayment);
        await _context.SaveChangesAsync();
    }
}