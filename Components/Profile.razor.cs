using magick.Services;
using magick.Models;
using Microsoft.AspNetCore.Components;

namespace magick.Components;

public partial class Profile
{
    [Inject] public UserService? UserService { get; set; } 

    private User? user;
    public bool IsLoggedIn { get; private set; } = false;
    
    [Inject] public AppState? AppState { get; set; }

    [Inject] public NavigationManager? NavigationManager { get; set; }

    protected override void OnInitialized()
    {
        UserService!.LoggedIn += OnLoggedInOut;
        UserService!.LoggedOut += OnLoggedInOut;
    }
    
    protected override async Task OnInitializedAsync()
    {
        user = await UserService!.GetUser();
        IsLoggedIn = user != null;
        AppState!.IsUserLoggedIn = IsLoggedIn;
    }
    
    private async void OnLoggedInOut(object? sender, EventArgs e)
    {
        user = await UserService!.GetUser();
        IsLoggedIn = user != null;
        AppState!.IsUserLoggedIn = IsLoggedIn;
        await InvokeAsync(() => 
        {
            AppState.NotifyStateChanged();
            StateHasChanged();
        });
    }

    public async void Logout() 
    {
        await UserService!.LogoutUser();
        user = null;
        IsLoggedIn = false;
        AppState!.IsUserLoggedIn = IsLoggedIn;
        await InvokeAsync(() => 
        {
            AppState.NotifyStateChanged();
            StateHasChanged();
        });
        NavigationManager?.NavigateTo("/");
    }

    public void Dispose()
    {
        UserService!.LoggedIn -= OnLoggedInOut;
        UserService!.LoggedOut -= OnLoggedInOut;
        GC.SuppressFinalize(this);
    }
}