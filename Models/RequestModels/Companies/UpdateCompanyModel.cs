using System.ComponentModel.DataAnnotations;

namespace FinalProjectAPBD.Models.RequestModels;

public class UpdateCompanyModel
{
    [Required] [MaxLength(100)] public string CompanyName { get; set; } = null!;

    [Required] [MaxLength(100)] public string Address { get; set; } = null!;

    [Required] [MaxLength(100)] public string Email { get; set; } = null!;

    [Required] [MaxLength(50)] public string PhoneNumber { get; set; } = null!;


    [Required] [MaxLength(50)] public string Krs { get; set; } = null!;
}