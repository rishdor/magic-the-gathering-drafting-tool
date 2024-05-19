using Microsoft.AspNetCore.Components;
using magick.Controllers;
using System.Threading.Tasks;

namespace magick.Components.Pages
{
    public partial class DecksBase : ComponentBase
    {
        [Inject]
        public DecksController? DecksController { get; set; }

        [Inject]
        public NavigationManager? NavManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await DecksController!.OnInitializedAsync();
        }

        public void NavigateToSetup()
        {
            NavManager!.NavigateTo("/setup");
        }
    }
}