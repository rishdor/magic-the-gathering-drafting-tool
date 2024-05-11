using magick.Models.Forms;
using magick.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace magick.Components.Pages;

public partial class Login {
    [Inject] public UserService? Service { get; set; }
    [Inject] public NavigationManager? Navigation { get; set; }
    [SupplyParameterFromForm] public UserLogin User { get; set; } = new();

    private string? errorMessage;

    private async Task HandleValidSubmit()
    {
        if (await Service!.LoginUser(User))
        {
            Navigation!.NavigateTo("/");
        }
        else
        {
            errorMessage = "Incorrect username or password";
        }
    }
}