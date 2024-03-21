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

    public async Task<User?> GetUser(string username)
    {
        User? user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        return user;
    }

    public async Task<List<Card>> GetCards()
    {
        return await _context.Cards.ToListAsync();
    }
    public async Task<List<Color>> GetColors()
    {
        return await _context.Colors.ToListAsync();
    }
    public async Task<bool> RegisterUser(User user)
    {
        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == user.Username);
        if (existingUser != null)
        {
            return false;
        }
        existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
        if (existingUser != null)
        {
            return false;
        }
        
        user.Password = PasswordHashing.HashPassword(user.Password);
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return true;
    }

    public List<Set> GetSets()
    {
        return _context.Sets.ToList();
    }
}
