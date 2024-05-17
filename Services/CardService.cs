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

    public async Task<List<Card>> GetCards(string searchTerm, string filterOption, int pageNumber, int pageSize)
    {
        using MagickContext context = _factory.CreateDbContext();
        var cardsQuery = context.Cards.AsQueryable();

        if (!string.IsNullOrEmpty(searchTerm))
        {
            cardsQuery = cardsQuery.Where(card => card.Name.Contains(searchTerm));
        }

        if (!string.IsNullOrEmpty(filterOption))
        {
            cardsQuery = cardsQuery.Where(card => card.Type == filterOption);
        }

        var paginatedCards = await cardsQuery
            .OrderBy(card => card.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(card => new Card { Name = card.Name, OriginalImageUrl = card.OriginalImageUrl })
            .ToListAsync();

        return paginatedCards;
    }

    public async Task<List<Card>> GetAllCards(int pageNumber, int pageSize)
    {
        using MagickContext context = _factory.CreateDbContext();
        return await context.Cards
            .OrderBy(card => card.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<List<Card>> GetCards(int pageNumber, int pageSize)
    {
        using MagickContext context = _factory.CreateDbContext();
        var cardsQuery = context.Cards.AsQueryable();

        var paginatedCards = await cardsQuery
            .OrderBy(card => card.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return paginatedCards;
    }

    public async Task<List<Card>> GetPaginatedCards(long lastCardId, int pageSize)
    {
        using MagickContext context = _factory.CreateDbContext();
        var cardsQuery = context.Cards.AsQueryable();

        var paginatedCards = await cardsQuery
            .Where(card => card.Id > lastCardId && !string.IsNullOrEmpty(card.OriginalImageUrl))
            .OrderBy(card => card.Id)
            .Take(pageSize)
            .Select(card => new Card { Name = card.Name, OriginalImageUrl = card.OriginalImageUrl })
            .ToListAsync();

        return paginatedCards;
    }
}
