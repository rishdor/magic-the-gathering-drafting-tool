using Microsoft.AspNetCore.Components;
using magick.Controllers;
using System.Threading.Tasks;
using magick.Models;

namespace magick.Components.Pages
{
    public partial class DecksBase : ComponentBase
    {
        [Inject]
        public DecksController? DecksController { get; set; }

        [Inject]
        public NavigationManager? NavManager { get; set; }
        protected bool showPopup = false;
        protected UserDeck selectedDeck;

        protected override async Task OnInitializedAsync()
        {
            await DecksController!.OnInitializedAsync();
        }

        public void NavigateToSetup()
        {
            NavManager!.NavigateTo("/setup");
        }
        public void OpenPopup(UserDeck deck)
        {
            selectedDeck = deck;
            showPopup = true;
        }

        public void ClosePopup()
        {
            showPopup = false;
        }
    }
}