using System.ComponentModel.DataAnnotations;

namespace FinalProjectAPBD.Models.RequestModels;

public class CreateCustomerContractModel
{
    [Required] public DateTime StratDate { get; set; }

    [Required] public DateTime EndDate { get; set; }

    [Required] public Decimal Price { get; set; }

    [Required] public int CustomerID { get; set; }

    [Required] public int SoftwareID { get; set; }
    [Required] public bool IsActive { get; set; }
    [Required] public int DiscountID { get; set; }
}