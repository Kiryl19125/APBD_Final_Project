using System;
using System.Collections.Generic;

namespace FinalProjectAPBD.Models;

public partial class Software
{
    public int SoftwareId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? CurrentVersion { get; set; }

    public string? Category { get; set; }

    public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();

    public virtual ICollection<ContractsCompany> ContractsCompanies { get; set; } = new List<ContractsCompany>();
}
