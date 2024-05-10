using magick.Models.Forms;
using magick.Services;
using Microsoft.AspNetCore.Components;
using RegistrationResult = magick.Services.UserService.RegistrationResult;

namespace magick.Components.Pages;

public partial class Register
{
    [Inject] public UserService? Service { get; set; }
    [SupplyParameterFromForm] public UserRegistration User { get; set; } = new();

    private string? errorMessage;

    private async Task HandleValidSubmit()
    {
        var result = await Service!.RegisterUser(User);
        switch(result) {
            case RegistrationResult.SUCCESS:
                errorMessage = string.Empty;
                User = new UserRegistration();
                break;
            case RegistrationResult.USERNAME_TAKEN:
                errorMessage = "User with this username already exists.";
                break;
            case RegistrationResult.EMAIL_TAKEN:
                errorMessage = "User with this email already exists.";
                break;
            case RegistrationResult.GENERAL_FAILURE:
                errorMessage = "An error occurred.";
                break;
        }
    }
}
