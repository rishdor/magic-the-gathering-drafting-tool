using magick.Services;
using System.Threading.Tasks;
using magick.Models;

namespace magick.Controllers
{
    public class CardDetailsPopupController
    {
        private readonly SetService _setService;
        private readonly CardService _cardService;
        private Card? _card;
        private long? _cardId;
        public bool IsVisible { get; private set; }
        public CardDetailsPopupController(SetService setService, CardService cardService)
        {
            _setService = setService;
            _cardService = cardService;
        }

        public void PrepareToShow(long cardId)
        {
            Close();
            _cardId = cardId;
        }

        public async Task Show()
        {
            if (_cardId.HasValue)
            {
                _card = await _cardService.GetCardById(_cardId.Value);
                IsVisible = true;
            }
        }

        public void Close()
        {
            _card = null;
        }

        public string? GetCardName()
        {
            return _card?.Name;
        }

        public string? GetCardImageUrl()
        {
            return _card?.OriginalImageUrl;
        }

        public string? GetCardManaCost()
        {
            return _card?.ManaCost;
        }

        public string? GetCardType()
        {
            return _card?.Type;
        }

        public string? GetCardRarityName()
        {
            return _cardService.RarityName(_card?.RarityCode);
        }

        public string? GetCardSetCode()
        {
            return _card?.SetCode;
        }

        public string? GetCardText()
        {
            return _card?.Text;
        }

        public string? GetSetName()
        {
            return _setService.GetSetName(_card?.SetCode);
        }
    }
}