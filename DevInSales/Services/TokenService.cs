using DevInSales.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DevInSales.Services
{
    public static class TokenService
    {

        // TODO: usar esse service por injeção de dependência.
        
        //private static IConfiguration _configuration;

        //static TokenService(IConfiguration config)
        //{
        //    _configuration = config;
        //}

        public static string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            // TODO: obter da configuração
            // var secret = _configuration["TokenConfigurations:SecretJwtKey"];
            var secret = "9fKuQPjkxtHMQrm3RKFq2jLqTFz4b3V25Ef8ah8wmeHPwnU2zxpgjx4XVArSs9an";

            var key = Encoding.ASCII.GetBytes(secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim("UserId", user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Name), // User.Identity.Name
                    new Claim(ClaimTypes.Role, user.Profile.Name) // User.IsInRole
                })
,
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials( new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


    }
}
