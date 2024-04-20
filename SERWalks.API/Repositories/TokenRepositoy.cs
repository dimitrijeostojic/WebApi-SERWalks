using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SERWalks.API.Repositories
{
    public class TokenRepositoy : ITokenRepository
    {
        // Polje za konfiguraciju radi pristupa postavkama aplikacije
        private readonly IConfiguration configuration;
        public TokenRepositoy(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // Metoda za kreiranje JWT tokena za korisnika sa specificiranim ulogama
        public string CreateJWTToken(IdentityUser user, List<string> roles)
        {
            // Kreiranje tvrdnji (claims) i dodavanje tvrdnje o email adresi korisnika
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Email, user.Email));

            // Dodavanje tvrdnji za uloge korisnika
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // Kreiranje ključa za potpisivanje tokena
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));

            // Kreiranje potvrda (credentials) za potpisivanje tokena
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Kreiranje JWT tokena sa specificiranim podacima
            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            // Pretvaranje tokena u string i vraćanje rezultata
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
