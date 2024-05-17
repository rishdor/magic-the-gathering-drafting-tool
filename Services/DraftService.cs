using Microsoft.EntityFrameworkCore;
using magick.Models;
using System.Collections.Immutable;

namespace magick.Services;

public class DraftService(CardService cardService, UserService userService, IDbContextFactory<MagickContext> factory)
{
    public static readonly int PACK_SIZE = 16;

    private readonly CardService _cardService = cardService;
    private readonly UserService _userService = userService;
    private readonly IDbContextFactory<MagickContext> _factory = factory;
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

    public async void FinishDraft(string deckName)
    {
        using MagickContext context = _factory.CreateDbContext();

        UserDeck deck = new() {
            UserId =  (await _userService.GetUser())!.Id,
            DeckName = deckName
        };
        Console.WriteLine(deck.Id);
        context.UserDecks.Add(deck);
        await context.SaveChangesAsync();
        Console.WriteLine(deck.Id);

        List<DeckCard> deckCards = (
            from card in GetDeck()
            select new DeckCard() {
                DeckId = deck.Id,
                CardId = card.Instance.Id
            }
        ).ToList();

        foreach (var deckCard in deckCards)
            context.DeckCards.Add(deckCard);

        await context.SaveChangesAsync();

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
