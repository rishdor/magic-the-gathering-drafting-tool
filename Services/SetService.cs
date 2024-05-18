using System.Threading.Tasks;
using magick.Models;
using Microsoft.EntityFrameworkCore;

namespace magick.Services
{
    public class SetService
    {
        private readonly MagickContext _context;

        public SetService(MagickContext context)
        {
            _context = context;
        }

        public async Task<Set> GetSetByCode(string code)
        {
            Set? set = await _context.Sets.FirstOrDefaultAsync(set => set.Code == code);
            return set!;
        }
    }
}