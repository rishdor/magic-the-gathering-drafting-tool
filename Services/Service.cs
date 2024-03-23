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

    //USERS
    public async Task<string> RegisterUser(User user)
    {
        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == user.Username);
        if (existingUser != null)
        {
            return "username exists";
        }
        existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
        if (existingUser != null)
        {
            return "email exists";
        }

        user.Password = PasswordHashing.HashPassword(user.Password);
        _context.Users.Add(user);
        var result = await _context.SaveChangesAsync();

        if (result > 0)
        {
            return "success";
        }
        else
        {
            return "failure";
        }
    }

    public async Task<string> LoginUser(User user)
    {
        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == user.Username);
        if (existingUser == null || !PasswordHashing.VerifyPassword(user.Password, existingUser.Password))
        {
            return "failure";
        }

        return "success";
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
