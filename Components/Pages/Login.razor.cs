using magick.Models.Forms;
using magick.Services;
using Microsoft.AspNetCore.Components;

namespace magick.Components.Pages;

public partial class Login {
    [Inject] public UserService? Service { get; set; }
    [SupplyParameterFromForm] public UserLogin User { get; set; } = new();

    private string? errorMessage;

    private async Task HandleValidSubmit()
    {
        if (await Service!.LoginUser(User))
        {
            errorMessage = string.Empty;
            User = new UserLogin();
            Console.WriteLine("Login successful");
            //create a session and redirect to home
        }
        else
        {
            errorMessage = "Incorrect username or password";
        }
    }
}