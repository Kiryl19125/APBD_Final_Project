namespace FinalProjectAPBD.Models.RequestModels;

public class CreateCompanyContractModel
{
    
    public DateTime StratDate { get; set; }
    public DateTime EndDate { get; set; }
    public Decimal Price { get; set; }
    public int CompanyID { get; set; }
    public int SoftwareID { get; set; }
    public bool IsActive { get; set; }
    public int DiscountID { get; set; }
}