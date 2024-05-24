using Microsoft.AspNetCore.Components;
using magick.Controllers;
using System.Threading.Tasks;
using magick.Models;
using System.Xml.Serialization;

namespace magick.Components
{
    public partial class DeckPopupComponent : ComponentBase
    {
        [Parameter]
        public UserDeck? SelectedDeck { get; set; }

        [Parameter]
        public EventCallback ClosePopup { get; set; }

        [Inject]
        protected CardGalleryController? controller { get; set; }

        protected override void OnInitialized()
        {
            controller!.LoadDeckCards(SelectedDeck!.Id);
        }

        protected void LoadDeckCards()
        {
            var moreCards = controller!.LoadDeckCards(SelectedDeck!.Id);
        }

        protected void OnSearch(CardFilterParameters parameters)
        {
            var cards = controller!.OnSearchDeck(SelectedDeck!.Id, parameters);
        }
        
        protected void OnFilter(CardFilterParameters parameters)
        {
            var cards = controller!.OnFilterDeck(SelectedDeck!.Id, parameters);
        }

        protected void OnReset()
        {
            controller!.OnDeckReset(SelectedDeck!.Id);
        }
    }
}