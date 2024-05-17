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

    public async Task<List<Card>> GetAllCards()
    {
        using MagickContext context = _factory.CreateDbContext();
        return await context.Cards.ToListAsync();
    }

    public async Task<List<Card>> GetCards(long lastCardId, int pageSize)
    {
        using MagickContext context = _factory.CreateDbContext();
        var cardsQuery = context.Cards.AsQueryable();

        var paginatedCards = await ApplyPagination(cardsQuery, lastCardId, pageSize);

        return paginatedCards;
    }

    public async Task<List<Card>> SearchCard(string query, long lastCardId, int pageSize)
    {
        using MagickContext context = _factory.CreateDbContext();
        var cardsQuery = context.Cards.AsQueryable();

        if (!string.IsNullOrEmpty(query))
        {
            cardsQuery = ApplySearch(cardsQuery, query, context);
        }

        var paginatedCards = await ApplyPagination(cardsQuery, lastCardId, pageSize);

        return paginatedCards;
    }

    private IQueryable<Card> ApplySearch(IQueryable<Card> cardsQuery, string query, MagickContext context)
    {
        return from card in cardsQuery
            join set in context.Sets on card.SetCode equals set.Code
            where card.Name.ToLower().Contains(query.ToLower()) || set.Name.ToLower().Contains(query.ToLower())
            select card;
    }

    private async Task<List<Card>> ApplyPagination(IQueryable<Card> cardsQuery, long lastCardId, int pageSize)
    {
        return await cardsQuery
            .Where(card => card.Id > lastCardId && !string.IsNullOrEmpty(card.OriginalImageUrl))
            .OrderBy(card => card.Id)
            .Take(pageSize)
            .ToListAsync();
    }
}