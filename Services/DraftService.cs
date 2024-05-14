using Microsoft.EntityFrameworkCore;
using magick.Models;
using System.Collections.Immutable;

namespace magick.Services;

public class DraftService(CardService cardService)
{
    public static readonly int PACK_SIZE = 16;

    private readonly CardService _cardService = cardService;
    private readonly List<Card> _table = [];
    private readonly List<Card> _deck = [];
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


    public List<Card> OpenPack()
    {
        var availableCards = _cardService.GetCardsFromSet(_setCode!);
        Card[] pack = Random.Shared
            .GetItems(availableCards.ToArray(), PACK_SIZE);
        _table.AddRange(pack);
        return pack.ToList();
    }


    public void AddCardToDeck(int cardTableIndex)
        => throw new NotImplementedException();

    public void RemoveCardFromDeck(int cardDeckIndex)
        => throw new NotImplementedException();


    public List<Card> GetTable()
        => _table;

    public List<Card> GetDeck()
        => _deck;
}
