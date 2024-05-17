using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace magick.Models.Forms;

public partial class UserRegistration
{
    [Required]
    [RegularExpression("^[A-Za-z0-9]+$")]
    public string Username { get; set; } = null!;

    [Required]
    [RegularExpression(
        pattern: @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$",
        ErrorMessage = "Password must contain at least 8 characters, one uppercase letter, one lowercase letter and one number")]
    public string Password { get; set; } = null!;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    [Compare("Password")]
    public string RepeatPassword {get; set; } = null!;
}