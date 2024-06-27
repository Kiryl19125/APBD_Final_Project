namespace FinalProjectAPBD.Models.RequestModels;

public class CreateCustomerContractModel
{
    public DateTime StratDate { get; set; }
    public DateTime EndDate { get; set; }
    public Decimal Price { get; set; }
    public int CustomerID { get; set; }
    public int SoftwareID { get; set; }
    public bool IsActive { get; set; }
    public int DiscountID { get; set; }
}