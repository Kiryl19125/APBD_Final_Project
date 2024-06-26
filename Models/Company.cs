using System;
using System.Collections.Generic;

namespace FinalProjectAPBD.Models;

public partial class Company
{
    public int CompanyId { get; set; }

    public string CompanyName { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Krs { get; set; } = null!;
}
