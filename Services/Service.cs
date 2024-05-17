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

}
