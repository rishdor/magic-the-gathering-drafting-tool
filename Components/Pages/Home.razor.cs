using magick.Models.Forms;
using magick.Services;
using magick.Models;
using Microsoft.AspNetCore.Components;

namespace magick.Components.Pages;

public partial class Home {
    [Inject] public UserService? UserService { get; set; }
    [Inject] public NavigationManager? NavManager { get; set; }

    private async Task Redirect()
    {
        var user = await UserService!.GetUser();
        if (user == null)
        {
            NavManager!.NavigateTo("/login");
        }
        else
        {
            NavManager!.NavigateTo("/setup");
        }
    }
}