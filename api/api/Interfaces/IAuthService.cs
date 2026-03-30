using api.Dto;
using api.Models;

namespace api.Interfaces;

public interface IAuthService
{
    public string GenerateJSONWebToken(UserDto userInfo);
    public bool VerifyPassword(string savedPasswordHash, string providedPassword);
    public string HashPassword(string password);
}