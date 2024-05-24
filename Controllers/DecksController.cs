using magick.Services;
using magick.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace magick.Controllers
{
    public class DecksController
    {
        private readonly DeckService _deckService;
        private readonly UserService _userService;
        private List<UserDeck> _decks = new List<UserDeck>();
        private Boolean _isLoading = false;

        public DecksController(DeckService deckService, UserService userService)
        {
            _deckService = deckService;
            _userService = userService;
        }

        public async Task OnInitializedAsync()
        {
            _isLoading = true;
            var user = await _userService.GetUser();
            if (user != null)
            {
                await GetDecks();
            }
            _isLoading = false;
        }

        public async Task GetDecks()
        {
            _decks = await _deckService.GetDecks();
        }

        public List<UserDeck> GetDecksList()
        {
            return _decks;
        }
        public Boolean IsLoading()
        {
            return _isLoading;
        }
    }
}