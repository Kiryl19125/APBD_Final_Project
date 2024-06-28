namespace FinalProjectAPBD.Models.RequestModels;

public class ProcessPaymentCompanyRequestModel
{
    
    public int ContractID { get; set; }
    public int CompanyID { get; set; }
    public DateTime PaymentDate { get; set; }
    public decimal Amount { get; set; }
}