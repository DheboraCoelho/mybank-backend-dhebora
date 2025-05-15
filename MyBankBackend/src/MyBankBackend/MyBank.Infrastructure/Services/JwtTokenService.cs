using System;
using System.Security.Claims;
using System.Text;

using MyBank.Domain.Entities;
using MyBank.Application.Interfaces;

namespace MyBank.Infrastructure.Services
{
    public class JwtTokenService : ITokenService
    {
        private readonly string _secretKey;
        private readonly int _expirationHours;

        public JwtTokenService(string secretKey, int expirationHours)
        {
            _secretKey = secretKey;
            _expirationHours = expirationHours;
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
                    new Claim(ClaimTypes.Email, user.Email.Value)
                }),
                Expires = DateTime.UtcNow.AddHours(_expirationHours),
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