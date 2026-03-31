using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using api.Dto;
using api.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace api.Services;

public class AuthService: IAuthService
{
    private IConfiguration _config;
    
    public AuthService(IConfiguration config)
    {
        _config = config;
    }
    
    public string GenerateJSONWebToken(UserDto userInfo)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Sub, userInfo.email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(_config["Jwt:Issuer"],
            _config["Jwt:Issuer"],
            claims,
            expires: DateTime.Now.AddMinutes(5),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string HashPassword(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(16);

        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 600000, HashAlgorithmName.SHA256);
        byte[] hash = pbkdf2.GetBytes(32);

        byte[] hashBytes = new byte[48]; 
        Array.Copy(salt, 0, hashBytes, 0, 16);
        Array.Copy(hash, 0, hashBytes, 16, 32);

        return Convert.ToBase64String(hashBytes);
    }

    public bool VerifyPassword(string savedPasswordHash, string providedPassword)
    {
        byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);

        byte[] salt = new byte[16];
        Array.Copy(hashBytes, 0, salt, 0, 16);

        var pbkdf2 = new Rfc2898DeriveBytes(providedPassword, salt, 600000, HashAlgorithmName.SHA256);
        byte[] hash = pbkdf2.GetBytes(32);

        byte[] actualHash = new byte[32];
        Array.Copy(hashBytes, 16, actualHash, 0, 32);

        return CryptographicOperations.FixedTimeEquals(actualHash, hash);
    }
}