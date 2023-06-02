using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace BlogApi.Services;

public class TokenService
{
    private readonly IConfiguration _configuration;
    private const int ExpirationMinutes = 30;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string CreateToken(IdentityUser user)
    {
        var expiration = DateTime.UtcNow.AddMinutes(ExpirationMinutes);
        var token = CreateJwtToken(
              CreateClaims(user),
              CreateSigningCredentials(),
              expiration
        );
        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }

    private JwtSecurityToken CreateJwtToken(List<Claim> claims, SigningCredentials credentials,
       DateTime expiration) =>
       new(
             "BlogApi",
             "BlogApi",
             claims,
             expires: expiration,
             signingCredentials: credentials
       );

    private List<Claim> CreateClaims(IdentityUser user)
    {
        try
        {
            var claims = new List<Claim>
               {
                  new Claim(JwtRegisteredClaimNames.Sub, "TokenForBlogApi"),
                  new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                  new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
                  new Claim(JwtRegisteredClaimNames.NameId, user.Id),
                  new Claim(JwtRegisteredClaimNames.Name, user.UserName!),
                  new Claim(JwtRegisteredClaimNames.Email, user.Email!)
               };
            return claims;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    private SigningCredentials CreateSigningCredentials()
    {
        return new SigningCredentials(
              new SymmetricSecurityKey(
                 Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings")["Secret"]!)
              ),
              SecurityAlgorithms.HmacSha256
        );
    }
}