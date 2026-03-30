using api.Models;

namespace api.Interfaces;

public interface IUserService
{
    public Task<User> CreateUser(User todoTask);
    public Task<User?> GetUserById(int userId);
    public Task<User?> GetUserByEmail(string email);
}
