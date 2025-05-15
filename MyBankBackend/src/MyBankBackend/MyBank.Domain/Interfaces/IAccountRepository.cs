using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MyBank.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace MyBank.Domain.Interfaces
{
    public interface IAccountRepository
    {
        Task<Account> GetByIdAsync(Guid id);
        Task<Account> GetByIdWithTransactionsAsync(Guid id);
        Task UpdateAsync(Account account);
        Task<bool> ExistsAsync(Guid accountId);
    }
}
