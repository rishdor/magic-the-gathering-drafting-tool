using magick.Services;
using magick.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using magick.Components;

namespace magick.Controllers
{
    public class DeckCardsController
    {
        private readonly DeckService _deckService;
        public List<Card>? Cards { get; private set; }
        public CardFilterParameters CurrentFilterParameters { get; private set; }

        public DeckCardsController(DeckService deckService)
        {
            _deckService = deckService;
            Cards = new List<Card>();
            CurrentFilterParameters = new CardFilterParameters();
        }

        public List<Card?> LoadDeckCards(int deckId)
        {
            List<Card?> cards;
        
            if (!string.IsNullOrEmpty(CurrentFilterParameters.SearchQuery))
            {
                cards = _deckService.SearchCardInDeck(deckId, CurrentFilterParameters.SearchQuery);
            }
            else if (CurrentFilterParameters.ConvertedManaCostFilter != null || CurrentFilterParameters.TypeFilter != null || CurrentFilterParameters.RarityCodeFilter != null || CurrentFilterParameters.ColorFilter != null)
            {
                cards = _deckService.FilterCardsInDeck(deckId, CurrentFilterParameters.SearchQuery, CurrentFilterParameters.ConvertedManaCostFilter, CurrentFilterParameters.TypeFilter, CurrentFilterParameters.RarityCodeFilter, CurrentFilterParameters.ColorFilter);
            }
            else
            {
                cards = _deckService.GetCards(deckId);
            }
            Cards!.Clear();
            Cards!.AddRange(cards!);
        
            return cards;
        }

        public List<Card?> OnSearchDeck(int deckId, CardFilterParameters parameters)
        {
            CurrentFilterParameters.SearchQuery = parameters.SearchQuery;
            var Cards = _deckService.SearchCardInDeck(deckId, parameters.SearchQuery);
            this.Cards = Cards!;

            return Cards;
        }

        public List<Card?> OnFilterDeck(int deckId, CardFilterParameters parameters)
        {
            CurrentFilterParameters.SearchQuery = parameters.SearchQuery;
            CurrentFilterParameters.ConvertedManaCostFilter = parameters.ConvertedManaCostFilter;
            CurrentFilterParameters.TypeFilter = parameters.TypeFilter;
            CurrentFilterParameters.RarityCodeFilter = parameters.RarityCodeFilter;
            CurrentFilterParameters.ColorFilter = parameters.ColorFilter;

            var Cards = _deckService.FilterCardsInDeck(deckId, parameters.SearchQuery, parameters.ConvertedManaCostFilter, parameters.TypeFilter, parameters.RarityCodeFilter, parameters.ColorFilter);
            this.Cards = Cards!;

            return Cards;
        }

        public void OnDeckReset(int deckId)
        {
            CurrentFilterParameters = new CardFilterParameters();
            Cards = new List<Card>();
            LoadDeckCards(deckId);
        }
    }
}