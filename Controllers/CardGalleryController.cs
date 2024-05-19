using magick.Services;
using magick.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using magick.Components;

namespace magick.Controllers
{
    public class CardGalleryController
    {
        private readonly CardService _service;
        private const int pageSize = 50;

        public List<Card>? Cards { get; private set; }
        public CardFilterParameters CurrentFilterParameters { get; private set; }

        public CardGalleryController(CardService service)
        {
            _service = service;
            Cards = new List<Card>();
            CurrentFilterParameters = new CardFilterParameters();
        }

        public async Task<List<Card>> LoadMoreCards()
        {
            string lastName = Cards!.Any() ? Cards!.Last().Name : string.Empty;
            List<Card> moreCards;

            if (!string.IsNullOrEmpty(CurrentFilterParameters.SearchQuery))
            {
                moreCards = await _service.SearchCard(CurrentFilterParameters.SearchQuery, lastName, pageSize);
            }
            else if (CurrentFilterParameters.ConvertedManaCostFilter != null || CurrentFilterParameters.TypeFilter != null || CurrentFilterParameters.RarityCodeFilter != null || CurrentFilterParameters.ColorFilter != null)
            {
                moreCards = await _service.FilterCards(CurrentFilterParameters.SearchQuery, lastName, pageSize, CurrentFilterParameters.ConvertedManaCostFilter, CurrentFilterParameters.TypeFilter, CurrentFilterParameters.RarityCodeFilter, CurrentFilterParameters.ColorFilter);
            }
            else
            {
                moreCards = await _service.GetCards(lastName, pageSize);
            }

            Cards!.AddRange(moreCards);
            return moreCards;
        }

        public async Task<List<Card>> OnSearch(CardFilterParameters parameters)
        {
            CurrentFilterParameters.SearchQuery = parameters.SearchQuery;
            var Cards = await _service.SearchCard(parameters.SearchQuery, "", pageSize);
            this.Cards = Cards;
            return Cards;
        }

        public async Task<List<Card>> OnFilter(CardFilterParameters parameters)
        {
            CurrentFilterParameters.SearchQuery = parameters.SearchQuery;
            CurrentFilterParameters.ConvertedManaCostFilter = parameters.ConvertedManaCostFilter;
            CurrentFilterParameters.TypeFilter = parameters.TypeFilter;
            CurrentFilterParameters.RarityCodeFilter = parameters.RarityCodeFilter;
            CurrentFilterParameters.ColorFilter = parameters.ColorFilter;

            var Cards = await _service.FilterCards(parameters.SearchQuery, "", pageSize, parameters.ConvertedManaCostFilter, parameters.TypeFilter, parameters.RarityCodeFilter, parameters.ColorFilter);
            this.Cards = Cards;
            return Cards;
        }

        public async Task OnReset()
        {
            CurrentFilterParameters = new CardFilterParameters();
            Cards = new List<Card>();
            await LoadMoreCards();
        }
    }
}