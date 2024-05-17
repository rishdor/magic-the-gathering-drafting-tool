using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using magick.Models;

namespace magick.Services;

public class Service
{
    private readonly MagickContext _context;

    public Service(MagickContext context)
    {
        _context = context;
    }

    public async Task<List<Card>> GetCards()
    {
        return await _context.Cards.ToListAsync();
    }

    public async Task<List<Color>> GetColors()
    {
        return await _context.Colors.ToListAsync();
    }
    


    //SETS
    public List<Set> GetSets()
    {
        return _context.Sets.ToList();
    }



    //CARDS
    public async Task<List<Card>> GetAllCards()
    {
        return await _context.Cards.ToListAsync();
    }

    public async Task<List<Card>> GetCards(string searchTerm, string filterOption, int pageNumber, int pageSize)
    {
        var cardsQuery = _context.Cards.AsQueryable();

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

}
