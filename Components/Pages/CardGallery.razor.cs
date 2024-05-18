using magick.Services;
using magick.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace magick.Components.Pages
{
    public partial class CardGallery : ComponentBase
    {
        [Inject]
        protected CardService? service { get; set; }

        protected List<Card>? cards;
        private const int pageSize = 50;
        protected string NoMatchingCardsError = "no-matching-cards-error";
        protected string NoMatchingCardsMessage = "";
        private CardFilterParameters currentFilterParameters = new CardFilterParameters();


        protected override async Task OnInitializedAsync()
        {
            cards = new List<Card>();
            await LoadMoreCards();
        }

        protected async Task LoadMoreCards()
        {
            NoMatchingCardsError = "no-matching-cards-error";
            string lastName = cards!.Any() ? cards!.Last().Name : string.Empty;

            List<Card> moreCards;
            
            if (!string.IsNullOrEmpty(currentFilterParameters.SearchQuery))
            {
                moreCards = await service!.SearchCard(currentFilterParameters.SearchQuery, lastName, pageSize);
            }
            else if (currentFilterParameters.ConvertedManaCostFilter != null || currentFilterParameters.TypeFilter != null || currentFilterParameters.RarityCodeFilter != null || currentFilterParameters.ColorFilter != null)
            {
                moreCards = await service!.FilterCards(currentFilterParameters.SearchQuery, lastName, pageSize, currentFilterParameters.ConvertedManaCostFilter, currentFilterParameters.TypeFilter, currentFilterParameters.RarityCodeFilter, currentFilterParameters.ColorFilter);
            }
            else
            {
                moreCards = await service!.GetCards(lastName, pageSize);
            }

            if (!moreCards.Any())
            {
                NoMatchingCardsError = "matching-cards-error";
                NoMatchingCardsMessage = "No more cards matching the criteria";
            }

            cards!.AddRange(moreCards);
        }

        protected async Task OnSearch(CardFilterParameters parameters)
        {
            currentFilterParameters.SearchQuery = parameters.SearchQuery;
            
            cards = await service!.SearchCard(parameters.SearchQuery, "", pageSize);

            if (!cards.Any())
            {
                NoMatchingCardsError = "matching-cards-error";
                NoMatchingCardsMessage = "No cards matching the criteria";
            }
        }
        
        protected async Task OnFilter(CardFilterParameters parameters)
        {
            currentFilterParameters.SearchQuery = parameters.SearchQuery;
            currentFilterParameters.ConvertedManaCostFilter = parameters.ConvertedManaCostFilter;
            currentFilterParameters.TypeFilter = parameters.TypeFilter;
            currentFilterParameters.RarityCodeFilter = parameters.RarityCodeFilter;
            currentFilterParameters.ColorFilter = parameters.ColorFilter;
        
            cards = await service!.FilterCards(parameters.SearchQuery, "", pageSize, parameters.ConvertedManaCostFilter, parameters.TypeFilter, parameters.RarityCodeFilter, parameters.ColorFilter);

            if (!cards.Any())
            {
                NoMatchingCardsError = "matching-cards-error";
                NoMatchingCardsMessage = "No cards matching the criteria";
            }
        }

        protected async Task OnReset()
        {
            currentFilterParameters = new CardFilterParameters();
            cards = new List<Card>();
            await LoadMoreCards();
        }
    }
}