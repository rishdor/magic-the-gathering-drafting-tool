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

    public List<Color> GetColors()
    {
        using MagickContext context = _factory.CreateDbContext();
        return context.Colors.ToList();
    }
    
    public List<Set> GetSets()
    {
        using MagickContext context = _factory.CreateDbContext();
        return context.Sets.ToList();
    }
}
