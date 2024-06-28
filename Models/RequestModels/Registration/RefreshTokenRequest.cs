using System.ComponentModel.DataAnnotations;

namespace FinalProjectAPBD.Models;

public class RefreshTokenRequest
{
    [Required]
    public string RefreshToken { get; set; }
}