﻿using System;
using System.Collections.Generic;

namespace FinalProjectAPBD.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Pesel { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();
}
