using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

using System;
using System.IdentityModel.Tokens.Jwt; // Adicione esta diretiva
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens; // Adicione esta diretiva
using MyBank.Domain.Entities;
using MyBank.Application.Interfaces;

namespace MyBank.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly string _secretKey;
        private readonly int _expirationHours;
        private readonly string _issuer;
        private readonly string _audience;

        public TokenService(string secretKey, int expirationHours, string issuer, string audience)
        {
            _secretKey = secretKey;
            _expirationHours = expirationHours;
            _issuer = issuer;
            _audience = audience;
        }

        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username)
                }),
                Expires = DateTime.UtcNow.AddHours(_expirationHours),
                Issuer = _issuer,
                Audience = _audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }

        public string GeneratePasswordResetToken()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
    }
}