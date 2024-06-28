using System.ComponentModel.DataAnnotations;

namespace FinalProjectAPBD.Models.RequestModels;

public class UpdateCustomerModel
{
    [Required] [MaxLength(100)] public string PESEL { get; set; }
    [Required] [MaxLength(100)] public string FirstName { get; set; }
    [Required] [MaxLength(100)] public string LastName { get; set; }
    [Required] [MaxLength(100)] public string Address { get; set; }
    [Required] [MaxLength(100)] public string Email { get; set; }
    [Required] [MaxLength(100)] public string PhoneNumber { get; set; }
}