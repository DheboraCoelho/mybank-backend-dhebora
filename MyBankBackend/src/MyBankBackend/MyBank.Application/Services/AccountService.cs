using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// MyBank.Application/Services/AccountService.cs
using MyBank.Domain.Account.Entities;
using MyBank.Application.DTOs;
using MyBank.Domain.Account.Interfaces;
using System;
using System.Threading.Tasks;
using MyBank.Application.Interfaces;
using MyBank.Domain.Account.ValueObjects.Account;
using MyBank.Domain.Account.ValueObjects.Shared;


namespace MyBank.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<AccountResponse> CreateAccount(CreateAccountRequest request)
        {
            var accountNumber = AccountNumber.Create(request.AccountNumber);
            var agency = Agency.Create(request.Agency);

            var account = new BankAccount(accountNumber, agency, request.OwnerId);
            await _accountRepository.AddAsync(account);

            return new AccountResponse
            {
                Id = account.Id,
                AccountNumber = account.Number.Value,
                Agency = account.Agency.Value,
                Balance = account.Balance.Amount,
                CreatedAt = account.CreatedAt,
                IsActive = account.IsActive
            };
        }

        public async Task<AccountResponse> GetAccount(string id)
        {
            var account = await _accountRepository.GetByIdAsync(id);
            if (account == null) return null;

            return new AccountResponse
            {
                Id = account.Id,
                AccountNumber = account.Number.Value,
                Agency = account.Agency.Value,
                Balance = account.Balance.Amount,
                CreatedAt = account.CreatedAt,
                IsActive = account.IsActive,
                Currency = account.Balance.Currency.ToString(),
                OwnerId = account.OwnerId
            };
        }

        public async Task Deposit(string accountId, TransactionRequest request)
        {
            var account = await _accountRepository.GetByIdAsync(accountId);
            if (account == null)
                throw new ArgumentException("Conta não encontrada");

            var amount = Money.Create(request.Amount);
            account.Deposit(amount);
            await _accountRepository.UpdateAsync(account);
        }

        public async Task Withdraw(string accountId, TransactionRequest request)
        {
            var account = await _accountRepository.GetByIdAsync(accountId);
            if (account == null)
                throw new ArgumentException("Conta não encontrada");

            var amount = Money.Create(request.Amount);
            account.Withdraw(amount);
            await _accountRepository.UpdateAsync(account);
        }

        public async Task DeactivateAccount(string accountId)
        {
            var account = await _accountRepository.GetByIdAsync(accountId);
            if (account == null)
                throw new ArgumentException("Conta não encontrada");

            account.Deactivate();
            await _accountRepository.UpdateAsync(account);
        }
    }
    }
