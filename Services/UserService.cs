using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using magick.Models;
using magick.Models.Forms;

namespace magick.Services;

public class UserService
{
    private readonly MagickContext _context;

    public UserService(MagickContext context)
    {
        _context = context;
    }

    public async Task<User?> GetUser(string username)
    {
        User? user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        return user;
    }

    public async Task<RegistrationResult> RegisterUser(UserRegistration user)
    {
        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == user.Username);
        if (existingUser != null) return RegistrationResult.USERNAME_TAKEN;

        existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
        if (existingUser != null) return RegistrationResult.EMAIL_TAKEN;

        User u = new User();
        u.Email = user.Email;
        u.Username = user.Username;
        u.Password = PasswordHashing.HashPassword(user.Password);
        _context.Users.Add(u);
        var result = await _context.SaveChangesAsync();

        return result > 0 ? RegistrationResult.SUCCESS
            : RegistrationResult.GENERAL_FAILURE;
    }

    public async Task<bool> LoginUser(UserLogin user)
    {
        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == user.Username);
        if (existingUser == null || !PasswordHashing.VerifyPassword(user.Password, existingUser.Password))
        {
            return false;
        }

        return true;
    }

    public enum RegistrationResult {
        SUCCESS,
        USERNAME_TAKEN,
        EMAIL_TAKEN,
        GENERAL_FAILURE
    }
}
