using magick.Services;
using magick.Models;
using Microsoft.AspNetCore.Components;

namespace magick.Components.Pages
{
    public partial class CardGallery : ComponentBase
    {
        [Inject]
        protected CardService? service { get; set; }

        protected List<Card>? cards;
        private long lastCardId = 0;
        private const int pageSize = 50;

        protected override async Task OnInitializedAsync()
        {
            await LoadMoreCards();
        }

        protected async Task LoadMoreCards()
        {
            var newCards = await service!.GetPaginatedCards(lastCardId, pageSize);
            if (cards == null)
            {
                cards = newCards;
            }
            else
            {
                cards.AddRange(newCards);
            }
            if (newCards.Any())
            {
                lastCardId = newCards.Last().Id;
            }
        }
    }
}