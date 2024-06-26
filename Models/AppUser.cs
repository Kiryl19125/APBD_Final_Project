using System;
using System.Collections.Generic;

namespace FinalProjectAPBD.Models;

public partial class AppUser
{
    public int AppUserId { get; set; }

    public string Login { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Salt { get; set; } = null!;

    public string RefreshToken { get; set; } = null!;

    public DateTime RefreshTockenExp { get; set; }
}
