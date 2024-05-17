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
        string searchQuery = "";

        protected override async Task OnInitializedAsync()
        {
            await LoadMoreCards();
        }

        protected async Task LoadMoreCards()
        {
            var newCards = await service!.GetCards(lastCardId, pageSize);
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

        protected async Task SearchCards()
        {
            var searchedCards = await service!.SearchCard(searchQuery, lastCardId, pageSize);
            if (cards == null)
            {
                cards = searchedCards;
            }
            else
            {
                cards.Clear();
                cards.AddRange(searchedCards);
            }
            if (searchedCards.Any())
            {
                lastCardId = searchedCards.Last().Id;
            }
        }
    }
}