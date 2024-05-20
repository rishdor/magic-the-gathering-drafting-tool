using System.Threading.Tasks;
using magick.Models;
using Microsoft.EntityFrameworkCore;

namespace magick.Services
{
    public class SetService(IDbContextFactory<MagickContext> factory)
    {
        private readonly IDbContextFactory<MagickContext> _factory = factory;


        public Set? GetSetByCode(string code)
        {
            using MagickContext context = _factory.CreateDbContext();
            return context.Sets.SingleOrDefault(set => set.Code == code);
        }

        public string GetSetName(string? setCode)
        {
            using MagickContext context = _factory.CreateDbContext();
            return context.Sets.FirstOrDefault(set => set.Code == setCode)?.Name ?? "";
        }

        public string RarityName(string? rarityCode)
        {
            using MagickContext context = _factory.CreateDbContext();
            return context.Rarities.FirstOrDefault(rarity => rarity.Code == rarityCode)?.Name ?? "";
        }

        public Rarity? GetRarityByCode(string code)
        {
            using MagickContext context = _factory.CreateDbContext();
            return context.Rarities.SingleOrDefault(rarity => rarity.Code == code);
        }
    }
}