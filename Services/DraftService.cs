using Microsoft.EntityFrameworkCore;
using magick.Models;
using System.Collections.Immutable;

namespace magick.Services;

public class DraftService(CardService cardService)
{
    public static readonly int PACK_SIZE = 16;

    private readonly CardService _cardService = cardService;
    private readonly Dictionary<Guid, Card> _table = [];
    private readonly Dictionary<Guid, bool> _deck = [];
    private string? _setCode = null;
    private bool _isDrafting = false;


    public void StartDraft(string setCode)
    {
        _table.Clear();
        _deck.Clear();
        _setCode = setCode;
        _isDrafting = true;
    }

    public void FinishDraft()
    {
        _table.Clear();
        _deck.Clear();
        _setCode = null;
        _isDrafting = false;
    }

    public bool IsDrafting()
        => _isDrafting;


    public void OpenPack()
    {
        var availableCards = _cardService.GetCardsFromSet(_setCode!);
        Card[] pack = Random.Shared
            .GetItems(availableCards.ToArray(), PACK_SIZE);
        foreach (Card card in pack) {
            Guid id = Guid.NewGuid();
            _table[id] = card;
            _deck[id] = false;
        }
    }


    public void AddCardToDeck(Guid cardUid)
    {
        if (!_table.ContainsKey(cardUid))
            throw new Exception("Card not found");
        _deck[cardUid] = true;
    }

    public void RemoveCardFromDeck(Guid cardUid)
    {
        if (!_table.ContainsKey(cardUid))
            throw new Exception("Card not found");
        _deck[cardUid] = false;
    }


    public List<Tagged<Card>> GetTable()
        => (from pair in _table
            where !_deck[pair.Key]
            select Tagged<Card>.FromPair(pair)
        ).ToList();

    public List<Tagged<Card>> GetDeck()
        => (from pair in _table
            where _deck[pair.Key]
            select Tagged<Card>.FromPair(pair)
        ).ToList();
}
