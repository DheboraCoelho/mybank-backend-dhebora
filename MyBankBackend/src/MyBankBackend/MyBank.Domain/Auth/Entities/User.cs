using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyBank.Domain.Account.ValueObjects.Shared;
using MyBank.Domain.ValueObjects.Shared;

namespace MyBank.Domain.Auth.Entities
{
    public class User
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string CPF { get; private set; }
        public string PasswordHash { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public Document Document { get; set; }

        public User(string name, string email, string cpf, string passwordHash)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            Email = email;
            CPF = cpf;
            PasswordHash = passwordHash;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
