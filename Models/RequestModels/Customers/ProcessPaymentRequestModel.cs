namespace FinalProjectAPBD.Models.RequestModels;

public class ProcessPaymentRequestModel
{
    public int ContractID { get; set; }
    public int CustomerID { get; set; }
    public DateTime PaymentDate { get; set; }
    public decimal Amount { get; set; }
}