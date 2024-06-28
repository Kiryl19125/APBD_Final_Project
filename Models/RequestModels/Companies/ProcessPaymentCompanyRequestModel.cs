using System.ComponentModel.DataAnnotations;

namespace FinalProjectAPBD.Models.RequestModels;

public class ProcessPaymentCompanyRequestModel
{
    [Required] public int ContractID { get; set; }

    [Required] public int CompanyID { get; set; }

    [Required] public DateTime PaymentDate { get; set; }

    [Required] public decimal Amount { get; set; }
}