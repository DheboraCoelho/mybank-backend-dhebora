using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MyBank.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBank.Domain.Interfaces
{
    public interface IAccountRepository
    {
        Task<Account> GetByIdAsync(Guid id);
        Task<Account> GetByIdWithTransactionsAsync(Guid id);
        Task<Account> GetByAccountNumberAsync(string accountNumber);
        Task<IEnumerable<Account>> GetByCustomerIdAsync(Guid customerId);
        Task AddAsync(Account account);
        Task UpdateAsync(Account account);
        Task<bool> ExistsAsync(string accountNumber);
    }
}