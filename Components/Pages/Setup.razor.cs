using magick.Models.Forms;
using Microsoft.AspNetCore.Components;
using magick.Controllers;
using magick.Models;

namespace magick.Components.Pages
{
    public partial class Setup
    {
        [Inject] public SetupController? SetupController { get; set; }
        [Inject] public NavigationManager? NavManager { get; set; }

        [Parameter] public SetQuery Query { get; set; } = new();

        private List<Set> sets = [];

        protected override async Task OnInitializedAsync()
        {
            var user = await SetupController!.GetUser();
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
            await SetupController!.FilterSets(Query);
            sets = SetupController.GetSets();
        }
    }
}