using System.Threading.Tasks;
using magick.Models;
using Microsoft.EntityFrameworkCore;

namespace magick.Services
{
    public class SetService(IDbContextFactory<MagickContext> factory)
    {
        private readonly IDbContextFactory<MagickContext> _factory = factory;


        public Set GetSetByCode(string code)
        {
            using MagickContext context = _factory.CreateDbContext();
            return context.Sets.Find(code)!;
        }

        public string GetSetName(string? setCode)
        {
            using MagickContext context = _factory.CreateDbContext();
            return context.Sets.FirstOrDefault(set => set.Code == setCode)?.Name ?? "";
        }
    }
}