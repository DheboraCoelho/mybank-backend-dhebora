
using MyBank.Domain.Enums;

using MyBank.Domain.ValueObjects;
using System;

namespace MyBank.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }

        // Propriedades para refresh token
        public string RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiry { get; set; }

        // Propriedades para reset de senha
        public string PasswordResetToken { get; set; }
        public DateTime? PasswordResetTokenExpiry { get; set; }

        // Construtor
        public User(string username, string email, string passwordHash)
        {
            Username = username;
            Email = email;
            PasswordHash = passwordHash;
        }
    }
}