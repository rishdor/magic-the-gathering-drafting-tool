using magick.Models.Forms;
using magick.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace magick.Components.Pages;

public partial class Login {
    [Inject] public UserService? UserService { get; set; }
    [Inject] public NavigationManager? Navigation { get; set; }
    [SupplyParameterFromForm] public UserLogin User { get; set; } = new();

    private string? errorMessage;

    protected override async Task OnInitializedAsync()
    {
        if (await UserService!.GetUser() != null)
            Navigation!.NavigateTo("/");
        await base.OnInitializedAsync();
    }

    private async Task HandleValidSubmit()
    {
        if (await UserService!.LoginUser(User))
        {
            Navigation!.NavigateTo("/");
        }
        else
        {
            errorMessage = "Incorrect username or password";
        }
    }
}