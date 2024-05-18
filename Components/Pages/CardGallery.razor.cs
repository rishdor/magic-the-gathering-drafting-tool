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
        string NoMatchingCardsError = "no-matching-cards-error";
        protected string NoMatchingCardsMessage = "";

        protected override async Task OnInitializedAsync()
        {
            cards = new List<Card>();
            await LoadMoreCards();
        }
        
        protected async Task SearchCards()
        {
            NoMatchingCardsError = "no-matching-cards-error";
            string lastName = string.Empty;
            allCards = await service!.SearchCard(searchQuery, lastName, pageSize);
            cards!.Clear();
            cards.AddRange(allCards.Take(pageSize));
        
            if (!allCards.Any())
            {
                NoMatchingCardsError = "matching-cards-error";
                NoMatchingCardsMessage = "No cards matching the criteria";
            }
        }
        
        protected async Task FilterCards()
        {
            NoMatchingCardsError = "no-matching-cards-error";
            string lastName = string.Empty;
            allCards = await service!.FilterCards(searchQuery, lastName, pageSize, convertedManaCostFilter, typeFilter, rarityCodeFilter, colorFilter);
            cards!.Clear();
            cards.AddRange(allCards.Take(pageSize));
        
            if (!allCards.Any())
            {
                NoMatchingCardsError = "matching-cards-error";
                NoMatchingCardsMessage = "No cards matching the criteria";
            }
        }
        
        protected async Task LoadMoreCards()
        {
            NoMatchingCardsError = "no-matching-cards-error";
            string lastName = cards!.Any() ? cards!.Last().Name : string.Empty;
            List<Card> moreCards;
        
            if (string.IsNullOrEmpty(searchQuery) && convertedManaCostFilter == null && typeFilter == null && rarityCodeFilter == null && colorFilter == null)
            {
                moreCards = await service!.GetCards(lastName, pageSize);
            }
            else
            {
                moreCards = await service!.FilterCards(searchQuery, lastName, pageSize, convertedManaCostFilter, typeFilter, rarityCodeFilter, colorFilter);
            }
        
            if (!moreCards.Any())
            {
                NoMatchingCardsError = "matching-cards-error";
                NoMatchingCardsMessage = "No more cards matching the criteria";
            }
        
            cards!.AddRange(moreCards);
        }
        
        protected async Task ResetSearch()
        {
            searchQuery = "";
            convertedManaCostFilter = null;
            typeFilter = null;
            rarityCodeFilter = null;
            colorFilter = null;
            NoMatchingCardsError = "no-matching-cards-error";
            allCards = null;
            cards!.Clear();
            await LoadMoreCards();
        }
    }
}