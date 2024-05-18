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
        List<Card>? allCards;

        protected override async Task OnInitializedAsync()
        {
            cards = new List<Card>();
            await LoadMoreCards();
        }

        protected async Task SearchCards()
        {
            allCards = await service!.SearchCard(searchQuery);
            cards!.Clear();
            cards.AddRange(allCards.Take(pageSize));
            lastCardId = cards.Last().Id;
        }

        protected async Task LoadMoreCards()
        {
            if (string.IsNullOrEmpty(searchQuery))
            {
                var moreCards = await service!.GetCards(lastCardId, pageSize);
                cards!.AddRange(moreCards);
                if (moreCards.Any())
                {
                    lastCardId = moreCards.Last().Id;
                }
            }
            else
            {
                var moreCards = allCards!.SkipWhile(card => card.Id <= lastCardId).Take(pageSize).ToList();
                cards!.AddRange(moreCards);
                if (moreCards.Any())
                {
                    lastCardId = moreCards.Last().Id;
                }
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