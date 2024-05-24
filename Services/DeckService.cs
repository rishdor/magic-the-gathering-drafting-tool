using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using magick.Models;
using Microsoft.EntityFrameworkCore.Internal;

namespace magick.Services;

public class DeckService(
    IDbContextFactory<MagickContext> factory,
    UserService userService)
{
    private readonly IDbContextFactory<MagickContext> _factory = factory;
    private readonly UserService _userService = userService;

    public List<Card?> GetCards(int deckId)
    {
        using MagickContext context = _factory.CreateDbContext();
        return (
            from deckCard in context.DeckCards
            where deckCard.DeckId == deckId
            select (
                from card in context.Cards
                where card.Id == deckCard.CardId
                select card
            ).First()
        ).ToList();
    }

    public async Task<List<UserDeck>> GetDecks()
    {
        using MagickContext context = _factory.CreateDbContext();
        User? user = await _userService.GetUser();
        if (user == null)
    {
        // Handle the case when user is null, e.g., throw an exception or return an empty list
        throw new Exception("User is not authenticated");
    }
        return await (
            from deck in context.UserDecks
            .Include(d => d.DeckCards).ThenInclude(dc => dc.Card)
            where deck.UserId == user!.Id
            select deck
        ).ToListAsync();
    }

    public bool DeleteDeck(int deckId)
    {
        using MagickContext context = _factory.CreateDbContext();
        UserDeck? deck = (from d in context.UserDecks
            where d.Id == deckId
            select d
        ).FirstOrDefault();

        if (deck != null)
        {
            List<Card?> cards = GetCards(deckId);
            context.Cards.RemoveRange(cards!);
            context.UserDecks.Remove(deck);
        }
        
        context.SaveChanges();
        return deck != null;
    }

    public List<Card?> SearchCardInDeck(int deckId, string query)
    {
        using MagickContext context = _factory.CreateDbContext();
        var cards = GetCards(deckId);
    
        if (!string.IsNullOrEmpty(query))
        {
            cards = (from card in cards
                     join set in context.Sets on card.SetCode equals set.Code
                     where card.Name.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                           set.Name.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                           set.Code.Contains(query, StringComparison.OrdinalIgnoreCase)
                     select card).ToList();
        }
    
        return cards ?? new List<Card?>();
    }

    public List<Card?> FilterCardsInDeck(int deckId, string query, int? convertedManaCost = null, string? type = null, string? rarityCode = null, string? color = null)
    {
        using MagickContext context = _factory.CreateDbContext();
        var cards = GetCards(deckId);
    
        if (convertedManaCost.HasValue)
        {
            cards = cards?.Where(card => card!.ConvertedManaCost == convertedManaCost.ToString()).ToList();
        }
    
        if (!string.IsNullOrEmpty(type))
        {
            cards = cards?.Where(card => card!.CardTypes.Any(ct => ct.Type.Name == type)).ToList();
        }
    
        if (!string.IsNullOrEmpty(rarityCode))
        {
            cards = cards?.Where(card => card!.RarityCode == rarityCode).ToList();
        }
    
        if (!string.IsNullOrEmpty(color))
        {
            cards = cards?.Where(card => card!.CardColors.Any(cc => cc.Color.Name == color)).ToList();
        }
    
        return cards?.OrderBy(card => card!.Name).ToList() ?? new List<Card?>();
    }
}
