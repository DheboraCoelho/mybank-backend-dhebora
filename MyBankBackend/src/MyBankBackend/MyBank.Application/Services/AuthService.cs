using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// MyBankAPI.Application/Services/AuthService.cs
using Microsoft.IdentityModel.Tokens;
using MyBank.Domain.Auth.Entities;
using MyBank.Domain.Auth.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using MyBank.Application.Interfaces;

namespace MyBank.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<string> Authenticate(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null || !VerifyPassword(password, user.PasswordHash))
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Email, user.Email)
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"]
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<bool> Register(string name, string email, string cpf, string password)
        {
            if (await _userRepository.GetByEmailAsync(email) != null ||
                await _userRepository.GetByCPFAsync(cpf) != null)
            {
                return false;
            }

            var passwordHash = HashPassword(password);
            var user = new User(name, email, cpf, passwordHash);
            await _userRepository.AddAsync(user);
            return true;
        }

        private string HashPassword(string password)
        {
            // Implementação real deveria usar BCrypt ou similar
            return password + "_hashed"; // Exemplo simplificado
        }

        private bool VerifyPassword(string password, string storedHash)
        {
            // Implementação real deveria verificar hash
            return HashPassword(password) == storedHash; // Exemplo simplificado
        }
    }
}