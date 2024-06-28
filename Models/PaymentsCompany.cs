using System;
using System.Collections.Generic;

namespace FinalProjectAPBD.Models;

public partial class PaymentsCompany
{
    public int PaymentId { get; set; }

    public int ContractId { get; set; }

    public DateTime PaymentDate { get; set; }

    public decimal Amount { get; set; }

    public virtual ContractsCompany Contract { get; set; } = null!;
}
