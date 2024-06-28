using System.ComponentModel.DataAnnotations;

namespace FinalProjectAPBD.Models.RequestModels;

public class AddNewCustomerModel
{
    [Required] [MaxLength(100)] public string FirstName { get; set; } = null!;

    [Required] [MaxLength(100)] public string LastName { get; set; } = null!;

    [Required] [MaxLength(100)] public string Address { get; set; } = null!;

    [Required] [MaxLength(100)] public string Email { get; set; } = null!;

    [Required] [MaxLength(50)] public string PhoneNumber { get; set; } = null!;


    [Required] [MaxLength(50)] public string Pesel { get; set; } = null!;
}