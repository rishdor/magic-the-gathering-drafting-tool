using magick.Controllers;
using Microsoft.AspNetCore.Components;

namespace magick.Components.Pages
{
    public partial class CardGallery : ComponentBase
    {
        [Inject]
        protected CardGalleryController? controller { get; set; }

        protected string NoMatchingCardsError = "no-matching-cards-error";
        protected string NoMatchingCardsMessage = "";

        protected override async Task OnInitializedAsync()
        {
            await controller!.LoadMoreCards();
        }

        protected async Task LoadMoreCards()
        {
            var moreCards = await controller!.LoadMoreCards();
        
            if (!moreCards.Any())
            {
                NoMatchingCardsError = "matching-cards-error";
                NoMatchingCardsMessage = "No more cards matching the criteria";
            }
        }

        protected async Task OnSearch(CardFilterParameters parameters)
        {
            var cards = await controller!.OnSearch(parameters);
        
            if (!cards.Any())
            {
                NoMatchingCardsError = "matching-cards-error";
                NoMatchingCardsMessage = "No cards matching the criteria";
            }
        }
        
        protected async Task OnFilter(CardFilterParameters parameters)
        {
            var cards = await controller!.OnFilter(parameters);
        
            if (!cards.Any())
            {
                NoMatchingCardsError = "matching-cards-error";
                NoMatchingCardsMessage = "No cards matching the criteria";
            }
        }

        protected async Task OnReset()
        {
            await controller!.OnReset();
        }
    }
}