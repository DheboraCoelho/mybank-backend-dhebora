using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MyBank.Domain.Entities;
using MyBank.Domain.ValueObjects;

using System;
using System.Threading.Tasks;


using System;
using System.Threading.Tasks;

namespace MyBank.Domain.Interfaces
{
    public interface IUserRepository
    {
        // Operações básicas
        Task<User> GetByIdAsync(Guid id);
        Task AddAsync(User user);
        Task UpdateAsync(User user);

        // Operações de autenticação
        Task<User> GetByUsernameAsync(string username);
        Task<User> GetByEmailAsync(string email);

        // Operações de token
        Task<User> GetByRefreshTokenAsync(string refreshToken);
        Task<User> GetByPasswordResetTokenAsync(string token);

        // Validações
        Task<bool> UsernameExistsAsync(string username);
        Task<bool> EmailExistsAsync(string email);
    }
}