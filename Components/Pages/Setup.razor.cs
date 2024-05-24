using magick.Models.Forms;
using magick.Services;
using magick.Models;
using Microsoft.AspNetCore.Components;

namespace magick.Components.Pages;

public partial class Setup {
    [Inject] public CardService? CardService { get; set; }
    [Inject] public UserService? UserService { get; set; }
    [Inject] public NavigationManager? NavManager { get; set; }
   
    [SupplyParameterFromForm] public SetQuery Query { get; set; } = new();

    private List<Set> sets = [];
    
    protected override async Task OnInitializedAsync()
    {
        var user = await UserService!.GetUser();
        if (user == null)
        {
            NavManager!.NavigateTo("/login");
        }
        else
        {
            await FilterSets();
        }

    }
    private async Task FilterSets()
    {
        sets = await CardService!.GetSets(Query.Text);
    }
}