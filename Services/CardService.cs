using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using magick.Models;
using Microsoft.EntityFrameworkCore.Internal;

namespace magick.Services;

public class CardService(IDbContextFactory<MagickContext> factory)
{
    private readonly IDbContextFactory<MagickContext> _factory = factory;

    public List<Card> GetCards()
    {
        using MagickContext context = _factory.CreateDbContext();
        return context.Cards.ToList();
    }

    public List<Card> GetCardsFromSet(string setCode)
    {
        using MagickContext context = _factory.CreateDbContext();
        return (from card in context.Cards
            where card.SetCode == setCode
            select card).ToList();
    }

    public List<Color> GetColors()
    {
        using MagickContext context = _factory.CreateDbContext();
        return context.Colors.ToList();
    }
    
    public List<Set> GetSets(string query)
    {
        using MagickContext context = _factory.CreateDbContext();
        return (from set in context.Sets
            where set.Name.ToLower().Contains(query.ToLower())
            select set).ToList();
    }

    public async Task<List<Card>> GetCards(string lastName, int pageSize)
    {
        using MagickContext context = _factory.CreateDbContext();
        IQueryable<Card> cardsQuery = context.Cards
            .Where(card => !string.IsNullOrEmpty(card.OriginalImageUrl))
            .OrderBy(card => card.Name);

        if (!string.IsNullOrEmpty(lastName))
        {
            cardsQuery = cardsQuery.Where(card => string.Compare(card.Name, lastName) > 0);
        }

        var paginatedCards = await cardsQuery
            .Take(pageSize)
            .ToListAsync();

        return paginatedCards;
    }

    public async Task<List<Card>> SearchCard(string query, string lastName, int pageSize)
    {
        using MagickContext context = _factory.CreateDbContext();
        IQueryable<Card> cardsQuery = context.Cards
            .Where(card => !string.IsNullOrEmpty(card.OriginalImageUrl))
            .OrderBy(card => card.Name);

        if (!string.IsNullOrEmpty(query))
        {
            cardsQuery = ApplySearch(cardsQuery, query, context);
        }

        if (!string.IsNullOrEmpty(lastName))
        {
            cardsQuery = cardsQuery.Where(card => string.Compare(card.Name, lastName) > 0);
        }

        var paginatedCards = await cardsQuery
            .Take(pageSize)
            .ToListAsync();

        return paginatedCards;
    }

    private IQueryable<Card> ApplySearch(IQueryable<Card> cardsQuery, string query, MagickContext context)
    {
        return from card in cardsQuery
                join set in context.Sets on card.SetCode equals set.Code
                where card.Name.ToLower().Contains(query.ToLower()) 
                        || set.Name.ToLower().Contains(query.ToLower())
                        || set.Code.ToLower().Contains(query.ToLower())
                select card;
    }
}