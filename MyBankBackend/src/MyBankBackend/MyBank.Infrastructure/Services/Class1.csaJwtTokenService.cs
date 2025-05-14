using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Infrastructure/Services/JwtTokenService.cs
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MyBank.Domain.Entities;
using MyBank.Domain.Interfaces;

namespace MyBank.Infrastructure.Services
{
    public class JwtTokenService : ITokenService
    {
        private readonly string _secretKey;
        private readonly string _issuer;
        private readonly int _expiryMinutes;

        public JwtTokenService(string secretKey, string issuer, int expiryMinutes)
        {
            _secretKey = secretKey;
            _issuer = issuer;
            _expiryMinutes = expiryMinutes;
        }

        public string GenerateToken(Customer customer)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, customer.Id.ToString()),
                    new Claim(ClaimTypes.Name, customer.FullName)
                }),
                Expires = DateTime.UtcNow.AddMinutes(_expiryMinutes),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature),
                Issuer = _issuer
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}