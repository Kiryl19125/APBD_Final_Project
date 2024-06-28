using System;
using System.Collections.Generic;

namespace FinalProjectAPBD.Models;

public partial class Discount
{
    public int DiscountId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal DiscountPercentage { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();

    public virtual ICollection<ContractsCompany> ContractsCompanies { get; set; } = new List<ContractsCompany>();
}
