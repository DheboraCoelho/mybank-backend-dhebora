using MyBank.Domain.Account.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// MyBankAPI.Core/Account/Interfaces/IAccountRepository.cs

using System.Threading.Tasks;

namespace MyBank.Domain.Account.Interfaces
{
    public interface IAccountRepository
    {
        Task<BankAccount> GetByIdAsync(string id);
        Task AddAsync(BankAccount account);
        Task UpdateAsync(BankAccount account);
        Task DeleteAsync(string id);
    }
}
