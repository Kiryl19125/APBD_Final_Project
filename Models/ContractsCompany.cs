using System;
using System.Collections.Generic;

namespace FinalProjectAPBD.Models;

public partial class ContractsCompany
{
    public int ContractId { get; set; }

    public int CompanyId { get; set; }

    public int SoftwareId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public decimal TotalAmount { get; set; }

    public decimal Payed { get; set; }

    public int? DiscountId { get; set; }

    public bool IsSigned { get; set; }

    public bool IsActive { get; set; }

    public virtual Company Company { get; set; } = null!;

    public virtual Discount? Discount { get; set; }

    public virtual ICollection<PaymentsCompany> PaymentsCompanies { get; set; } = new List<PaymentsCompany>();

    public virtual Software Software { get; set; } = null!;
}
