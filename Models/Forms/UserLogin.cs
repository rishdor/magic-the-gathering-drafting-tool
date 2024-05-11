using System;
using System.Collections.Generic;
using  System.ComponentModel.DataAnnotations;

namespace magick.Models.Forms;

public partial class UserLogin
{
    [Required]
    [RegularExpression("^[A-Za-z0-9]+$")]
    public string Username { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;
}
