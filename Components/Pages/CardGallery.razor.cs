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
        private const int pageSize = 50;
        string searchQuery = "";
        List<Card>? allCards;

        protected override async Task OnInitializedAsync()
        {
            cards = new List<Card>();
            await LoadMoreCards();
        }

        protected async Task SearchCards()
        {
            string lastName = string.Empty;
            allCards = await service!.SearchCard(searchQuery, lastName, pageSize);
            cards!.Clear();
            cards.AddRange(allCards.Take(pageSize));
        }
        
        protected async Task LoadMoreCards()
        {
            string lastName = cards!.Any() ? cards!.Last().Name : string.Empty;
            if (string.IsNullOrEmpty(searchQuery))
            {
                var moreCards = await service!.GetCards(lastName, pageSize);
                cards!.AddRange(moreCards);
            }
            else
            {
                var moreCards = await service!.SearchCard(searchQuery, lastName, pageSize);
                cards!.AddRange(moreCards);
            }
        }

        protected async Task ResetSearch()
        {
            searchQuery = "";
            allCards = null;
            cards!.Clear();
            await LoadMoreCards();
        }
    }
}