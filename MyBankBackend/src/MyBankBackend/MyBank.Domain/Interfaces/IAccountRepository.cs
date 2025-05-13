using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MyBank.Domain.Entities;

namespace MyBank.Domain.Interfaces
{
    public interface IAccountRepository
    {
        Task<Account?> GetByIdAsync(Guid id);
        Task<Account?> GetByNumberAsync(string number);
        Task AddAsync(Account account);
        Task UpdateAsync(Account account);
        Task<bool> ExistsAsync(string accountNumber);
    }
}
