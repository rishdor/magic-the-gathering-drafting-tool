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

    public List<Card> GetCards(int deckId)
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
            List<Card> cards = GetCards(deckId);
            context.Cards.RemoveRange(cards);
            context.UserDecks.Remove(deck);
        }
        
        context.SaveChanges();
        return deck != null;
    }
}
