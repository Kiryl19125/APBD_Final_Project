using System.ComponentModel.DataAnnotations;

namespace FinalProjectAPBD.Models;

public class RegisterRequest
{
    [Required] [MaxLength(100)] public string Email { get; set; }
    [Required] [MaxLength(100)] public string Login { get; set; }
    [Required] [MaxLength(100)] public string Password { get; set; }
    [Required] [MaxLength(100)] public string Role { get; set; }
}