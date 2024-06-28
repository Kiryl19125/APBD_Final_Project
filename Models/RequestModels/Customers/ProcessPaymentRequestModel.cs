using System.ComponentModel.DataAnnotations;

namespace FinalProjectAPBD.Models.RequestModels;

public class ProcessPaymentRequestModel
{
    [Required] public int ContractID { get; set; }
    [Required] public int CustomerID { get; set; }
    [Required] public DateTime PaymentDate { get; set; }
    [Required] public decimal Amount { get; set; }
}