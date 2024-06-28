using System.ComponentModel.DataAnnotations;

namespace FinalProjectAPBD.Models.RequestModels;

public class DeleteCustomerModel
{
    [Required] [MaxLength(50)] public string PESEL { get; set; }
}