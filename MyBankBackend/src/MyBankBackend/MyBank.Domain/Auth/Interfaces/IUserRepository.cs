using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// MyBank.Domain/Auth/Interfaces/IUserRepository.cs
using MyBank.Domain.Auth.Entities;
using System.Threading.Tasks;


namespace MyBank.Domain.Auth.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(string id);
        Task<User> GetByEmailAsync(string email);
        Task<User> GetByCPFAsync(string cpf);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
    }
}