using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// MyBankAPI.Application/Interfaces/IAccountService.cs
using MyBank.Application.DTOs;
using System.Threading.Tasks;

namespace MyBank.Application.Interfaces
{
    public interface IAccountService
    {
        Task<AccountResponse> CreateAccount(CreateAccountRequest request);
        Task<AccountResponse> GetAccount(string id);
        Task Deposit(string accountId, TransactionRequest request);
        Task Withdraw(string accountId, TransactionRequest request);
        Task DeactivateAccount(string accountId);
    }
}
