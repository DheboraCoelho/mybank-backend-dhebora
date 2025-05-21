using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBank.Domain.Account.Services
{
    public interface IAccountService
    {
        Task<string> CreateAccountAsync(string accountNumber);
        Task<decimal> GetBalanceAsync(string accountId);
        Task DepositAsync(string accountId, decimal amount);
        Task WithdrawAsync(string accountId, decimal amount);
    }
}
