using System.ComponentModel.DataAnnotations;

namespace FinalProjectAPBD.Models;

public class LoginRequest
{
    [Required] [MaxLength(100)] public string Login { get; set; }
    [Required] public string Password { get; set; }
}