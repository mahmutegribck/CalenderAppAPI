using CalenderApp.Application.Interfaces.Tokens;
using CalenderApp.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CalenderApp.Infrastructure.Tokens
{
    public class JwtServisi(IConfiguration configuration) : IJwtServisi
    {
        public string JwtTokenOlustur(Kullanici kullanici)
        {
            var tokenhandler = new JwtSecurityTokenHandler();
            SymmetricSecurityKey key =
                new(Encoding.UTF8.GetBytes(configuration["JWT:Key"] ?? string.Empty));

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, kullanici.Id),
            };
            var tokendesc = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Audience = configuration["Jwt:Audience"],
                Issuer = configuration["Jwt:Issuer"],
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(
                    key,
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenhandler.CreateToken(tokendesc);
            var finaltoken = tokenhandler.WriteToken(token);

            return finaltoken;

        }
    }
}
