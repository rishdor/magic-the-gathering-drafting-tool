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
        int? convertedManaCostFilter = null;
        string? typeFilter = null;
        string? rarityCodeFilter = null;
        string? colorFilter = null;
        string NoMatchingCardsError = "";

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

        protected async Task FilterCards()
        {
            string lastName = string.Empty;
            allCards = await service!.FilterCards(searchQuery, lastName, pageSize, convertedManaCostFilter, typeFilter, rarityCodeFilter, colorFilter);
            cards!.Clear();
            cards.AddRange(allCards.Take(pageSize));
        }
        
        protected async Task LoadMoreCards()
        {
            string lastName = cards!.Any() ? cards!.Last().Name : string.Empty;
            List<Card> moreCards;
        
            if (string.IsNullOrEmpty(searchQuery))
            {
                NoMatchingCardsError = "visible";
                moreCards = await service!.GetCards(lastName, pageSize);
            }
            else
            {
                moreCards = await service!.FilterCards(searchQuery, lastName, pageSize, convertedManaCostFilter, typeFilter, rarityCodeFilter, colorFilter);
            }
        
            Console.WriteLine($"LoadMoreCards: {moreCards.Count} more cards found"); // Add logging
        
            if (!moreCards.Any())
            {
                // NoMatchingCardsError = "visible";
                return;
            }
        
            cards!.AddRange(moreCards);
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