using magick.Models;
using magick.Services;
using System.Collections.Generic;

namespace magick.Controllers
{
    public class DraftController
    {
        private readonly DraftService _draftService;
        private List<Tagged<Card>> _table = new List<Tagged<Card>>();
        private List<Tagged<Card>> _deck = new List<Tagged<Card>>();

        public DraftController(DraftService draftService)
        {
            _draftService = draftService;
        }

        public void StartDraft(string setCode)
        {
            _draftService.StartDraft(setCode);
            _table = _draftService.GetTable();
            _deck = _draftService.GetDeck();
        }

        public void OpenPack()
        {
            _draftService.OpenPack();
            _table = _draftService.GetTable();
            _deck = _draftService.GetDeck();
        }

        public void AddCardToDeck(Guid cardUid)
        {
            _draftService.AddCardToDeck(cardUid);
            _table = _draftService.GetTable();
            _deck = _draftService.GetDeck();
        }

        public void RemoveCardFromDeck(Guid cardUid)
        {
            _draftService.RemoveCardFromDeck(cardUid);
            _table = _draftService.GetTable();
            _deck = _draftService.GetDeck();
        }

        public void FinishDraft(string deckName)
        {
            _draftService.FinishDraft(deckName);
        }

        public List<Tagged<Card>> GetTable()
        {
            return _table;
        }

        public List<Tagged<Card>> GetDeck()
        {
            return _deck;
        }
    }
}