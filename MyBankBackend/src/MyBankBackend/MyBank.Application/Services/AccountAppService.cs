// Application/Services/AccountAppService.cs
using MyBank.Application.DTOs.Accounts;
using MyBank.Application.DTOs.Shared.MyBank.Application.DTOs.Shared;
using MyBank.Application.Interfaces;
using MyBank.Domain.Entities;
using MyBank.Domain.Enums;
using MyBank.Domain.Exceptions;
using MyBank.Domain.Repositories;
using MyBank.Domain.ValueObjects;

namespace MyBank.Application.Services
{
    public class AccountAppService : IAccountAppService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountAppService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<AccountResponse?> GetAccountByIdAsync(Guid accountId)
        {
            var account = await _accountRepository.GetByIdAsync(accountId);
            if (account == null)
                return null;

            var transactions = account.Transactions.Select(t => new TransactionResponse(
                t.Id,
                t.Amount,
                t.Type.ToString(),
                t.Date,
                t.Description
            ));

            return new AccountResponse(
                account.Id,
                account.Number,
                account.Balance.Value,
                account.Balance.Currency.ToString(),
                account.CustomerId,
                account.CreatedAt,
                transactions
            );
        }

        public async Task<TransactionResponse?> DepositAsync(TransactionRequest request)
        {
            var account = await _accountRepository.GetByIdAsync(request.AccountId);
            if (account == null)
                return null;

            var amount = new Amount(request.Amount, CurrencyType.BRL);
            account.Deposit(amount, request.Description);
            await _accountRepository.UpdateAsync(account);

            var transaction = account.Transactions.Last();

            return new TransactionResponse(
                transaction.Id,
                transaction.Amount,
                transaction.Type.ToString(),
                transaction.Date,
                transaction.Description
            );
        }

        public async Task<TransactionResponse?> WithdrawAsync(WithdrawRequest request)
        {
            var account = await _accountRepository.GetByIdAsync(request.AccountId);
            if (account == null)
                return null;

            var amount = new Amount(request.Amount, CurrencyType.BRL);
            account.Withdraw(amount, request.Description);
            await _accountRepository.UpdateAsync(account);

            var transaction = account.Transactions.Last();

            return new TransactionResponse(
                transaction.Id,
                transaction.Amount,
                transaction.Type.ToString(),
                transaction.Date,
                transaction.Description
            );
        }

        public async Task<TransactionResponse?> TransferAsync(TransferRequest request)
        {
            var sourceAccount = await _accountRepository.GetByIdAsync(request.SourceAccountId);
            var destinationAccount = await _accountRepository.GetByIdAsync(request.DestinationAccountId);

            if (sourceAccount == null || destinationAccount == null)
                return null;

            var amount = new Amount(request.Amount, CurrencyType.BRL);
            sourceAccount.TransferTo(destinationAccount, amount, request.Description);

            await _accountRepository.UpdateAsync(sourceAccount);
            await _accountRepository.UpdateAsync(destinationAccount);

            var transaction = sourceAccount.Transactions.Last();

            return new TransactionResponse(
                transaction.Id,
                transaction.Amount,
                transaction.Type.ToString(),
                transaction.Date,
                transaction.Description
            );
        }

        public async Task<IEnumerable<TransactionResponse>> GetStatementAsync(Guid accountId, DateRangeRequest period)
        {
            var transactions = await _accountRepository.GetTransactionsAsync(accountId, period.StartDate, period.EndDate);

            return transactions.Select(t => new TransactionResponse(
                t.Id,
                t.Amount,
                t.Type.ToString(),
                t.Date,
                t.Description
            ));
        }

        public async Task<decimal> GetCurrentBalanceAsync(Guid accountId)
        {
            var account = await _accountRepository.GetByIdAsync(accountId);
            return account?.Balance.Value ?? 0;
        }
    }
}
