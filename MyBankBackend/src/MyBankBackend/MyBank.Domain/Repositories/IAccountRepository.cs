using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Domain/Repositories/IAccountRepository.cs
using MyBank.Domain.Entities;

namespace MyBank.Domain.Repositories
{
    public interface IAccountRepository
    {
        Task<Account?> GetByIdAsync(Guid accountId);
        Task UpdateAsync(Account account);
        Task<IEnumerable<Transaction>> GetTransactionsAsync(Guid accountId, DateTime startDate, DateTime endDate);
    }
}

