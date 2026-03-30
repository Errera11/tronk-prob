using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Services;

public class UserService: IUserService
{
    private readonly UserContext _userContext;

    public UserService(UserContext context)
    {
        _userContext = context;
    }
    
    public async Task<User> CreateUser(User user)
    {
        var createdUser = await _userContext.Users.AddAsync(user);
        
        await _userContext.SaveChangesAsync();

        return createdUser.Entity;
    }

    public async Task<User?> GetUserById(int userId)
    {
        var user = await _userContext.Users.FindAsync(userId);

        return user;
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        var user = await _userContext.Users.Where(u => u.Email == email).FirstOrDefaultAsync();

        return user;
    }
}