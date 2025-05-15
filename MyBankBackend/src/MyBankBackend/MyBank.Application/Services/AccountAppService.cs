using AutoMapper;
using MyBank.Application.DTOs.Accounts;
using MyBank.Application.DTOs.Shared;
using MyBank.Application.Interfaces;
using MyBank.Domain.Entities;
using MyBank.Domain.Enums;
using MyBank.Domain.Exceptions;
using MyBank.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBank.Application.Services
{
    public class AccountAppService : IAccountAppService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AccountAppService(
            IAccountRepository accountRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<AccountResponse> GetAccountByIdAsync(Guid accountId)
        {
            var account = await _accountRepository.GetByIdAsync(accountId);
            if (account == null)
                throw new DomainException("Conta não encontrada");

            return _mapper.Map<AccountResponse>(account);
        }

        public async Task<TransactionResponse> DepositAsync(TransactionRequest request)
        {
            var account = await _accountRepository.GetByIdAsync(request.AccountId);
            if (account == null)
                throw new DomainException("Conta não encontrada");

            account.Deposit(request.Amount);
            await _accountRepository.UpdateAsync(account);

            return new TransactionResponse
            {
                TransactionId = Guid.NewGuid(),
                AccountId = account.Id,
                Amount = request.Amount,
                Type = TransactionType.Deposit.ToString(),
                Date = DateTime.UtcNow,
                Description = request.Description
            };
        }

        public async Task<TransactionResponse> WithdrawAsync(WithdrawRequest request)
        {
            var account = await _accountRepository.GetByIdAsync(request.AccountId);
            if (account == null)
                throw new DomainException("Conta não encontrada");

            account.Withdraw(request.Amount);
            await _accountRepository.UpdateAsync(account);

            return new TransactionResponse
            {
                TransactionId = Guid.NewGuid(),
                AccountId = account.Id,
                Amount = request.Amount,
                Type = TransactionType.Withdrawal.ToString(),
                Date = DateTime.UtcNow,
                Description = request.Description
            };
        }

        public async Task<TransactionResponse> TransferAsync(TransferRequest request)
        {
            var sourceAccount = await _accountRepository.GetByIdAsync(request.FromAccountId);
            var destinationAccount = await _accountRepository.GetByIdAsync(request.ToAccountId);

            if (sourceAccount == null || destinationAccount == null)
                throw new DomainException("Conta de origem ou destino não encontrada");

            sourceAccount.Transfer(destinationAccount, request.Amount);

            await _accountRepository.UpdateAsync(sourceAccount);
            await _accountRepository.UpdateAsync(destinationAccount);

            return new TransactionResponse
            {
                TransactionId = Guid.NewGuid(),
                AccountId = sourceAccount.Id,
                Amount = request.Amount,
                Type = TransactionType.Transfer.ToString(),
                Date = DateTime.UtcNow,
                Description = request.Description
            };
        }

        public async Task<IEnumerable<TransactionResponse>> GetStatementAsync(Guid accountId, DateRangeRequest period)
        {
            var account = await _accountRepository.GetByIdWithTransactionsAsync(accountId);
            if (account == null)
                throw new DomainException("Conta não encontrada");

            var transactions = account.Transactions
                .Where(t => t.CreatedAt >= period.StartDate && t.CreatedAt <= period.EndDate)
                .OrderByDescending(t => t.CreatedAt);

            return _mapper.Map<IEnumerable<TransactionResponse>>(transactions);
        }

        public async Task<decimal> GetCurrentBalanceAsync(Guid accountId)
        {
            var account = await _accountRepository.GetByIdAsync(accountId);
            if (account == null)
                throw new DomainException("Conta não encontrada");

            return account.Balance;
        }
    }
}