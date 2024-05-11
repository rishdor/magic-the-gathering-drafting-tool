using magick.Services;
using magick.Models;
using Microsoft.AspNetCore.Components;

namespace magick.Components;

public partial class Profile
{
    [Inject] public UserService? UserService { get; set; } 

    private User? user;

    protected override void OnInitialized()
    {
        UserService!.LoggedIn += OnLoggedInOut;
        UserService!.LoggedOut += OnLoggedInOut;
    }
    
    protected override async Task OnInitializedAsync()
    => user = await UserService!.GetUser();

    private async void OnLoggedInOut(object? sender, EventArgs e)
    {
        user = await UserService!.GetUser();
        StateHasChanged();
    }

    private async void Logout() {
        await UserService!.LogoutUser();
    }

    public void Dispose()
    {
        UserService!.LoggedIn -= OnLoggedInOut;
        UserService!.LoggedOut -= OnLoggedInOut;
        GC.SuppressFinalize(this);
    }
}
