using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using magick.Models;
using magick.Models.Forms;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace magick.Services;

public class UserService(MagickContext context, ProtectedLocalStorage localStorage)
{
    public event EventHandler? LoggedIn;
    public event EventHandler? LoggedOut;

    private readonly MagickContext _context = context;
    private readonly ProtectedLocalStorage _localStorage = localStorage;
    private User? _user;

    public async Task<User?> GetUser()
    {
        if (_user == null) {
            var result = await _localStorage.GetAsync<string>("user");
            if (result.Success && result.Value != null) _user = await GetUser(result.Value);
        }

        return _user;
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

        User u = new() {
            Email = user.Email,
            Username = user.Username,
            Password = PasswordHashing.HashPassword(user.Password)
        };

        _context.Users.Add(u);
        var result = await _context.SaveChangesAsync();

        return result > 0 ? RegistrationResult.SUCCESS
            : RegistrationResult.GENERAL_FAILURE;
    }

    public async Task<bool> LoginUser(UserLogin user)
    {
        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == user.Username);
        if (existingUser == null || !PasswordHashing.VerifyPassword(user.Password, existingUser.Password))
            return false;
        
        await _localStorage.SetAsync("user", user.Username);
        LoggedIn?.Invoke(this, EventArgs.Empty);
        return true;
    }

    public async Task<bool> LogoutUser() {
        bool wasLoggedIn = await GetUser() != null;
        _user = null;
        await _localStorage.DeleteAsync("user");
        LoggedOut?.Invoke(this, EventArgs.Empty);
        return wasLoggedIn;
    }


    public enum RegistrationResult {
        SUCCESS,
        USERNAME_TAKEN,
        EMAIL_TAKEN,
        GENERAL_FAILURE
    }
}
