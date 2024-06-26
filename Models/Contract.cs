﻿using System;
using System.Collections.Generic;

namespace FinalProjectAPBD.Models;

public partial class Contract
{
    public int ContractId { get; set; }

    public int CustomerId { get; set; }

    public int SoftwareId { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public decimal TotalAmount { get; set; }

    public int? DiscountId { get; set; }

    public bool IsSigned { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Discount? Discount { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Software Software { get; set; } = null!;
}
