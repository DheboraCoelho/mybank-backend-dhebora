using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using MyBank.Domain.Entities;
using MyBank.Domain.Interfaces;
using MyBank.Application.DTOs.Accounts;
using MyBank.Domain.Exceptions;
using MyBank.Domain.ValueObjects;
using MyBank.Domain.Enums;

namespace MyBank.Application.Services
{
    public class AccountAppService : IAccountAppService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;
        private readonly INotificationSender _notificationSender;

        public AccountAppService(
            IAccountRepository accountRepository,
            IMapper mapper,
            INotificationSender notificationSender)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
            _notificationSender = notificationSender;
        }

        public async Task<AccountResponse> GetAccountByIdAsync(Guid accountId)
        {
            var account = await _accountRepository.GetByIdAsync(accountId)
                ?? throw new DomainException("Conta não encontrada");

            return _mapper.Map<AccountResponse>(account);
        }

        public async Task<TransactionResponse> DepositAsync(TransactionRequest request)
        {
            var account = await _accountRepository.GetByIdAsync(request.AccountId)
                ?? throw new DomainException("Conta não encontrada");

            var amount = new Amount(request.Amount, CurrencyType.BRL);
            account.Deposit(amount);

            await _accountRepository.UpdateAsync(account);

            // Notificação
            await _notificationSender.SendAsync(
                new Notification(
                    $"Depósito realizado",
                    $"Valor: {amount}",
                    NotificationType.Transaction,
                    account.CustomerId));

            return _mapper.Map<TransactionResponse>(account.Transactions.Last());
        }

        public async Task<TransactionResponse> TransferAsync(TransferRequest request)
        {
            var sourceAccount = await _accountRepository.GetByIdAsync(request.SourceAccountId)
                ?? throw new DomainException("Conta origem não encontrada");

            var targetAccount = await _accountRepository.GetByIdAsync(request.TargetAccountId)
                ?? throw new DomainException("Conta destino não encontrada");

            var amount = new Amount(request.Amount, CurrencyType.BRL);

            sourceAccount.Withdraw(amount);
            targetAccount.Deposit(amount);

            await _accountRepository.UpdateAsync(sourceAccount);
            await _accountRepository.UpdateAsync(targetAccount);

            // Notificações
            await _notificationSender.SendAsync(
                new Notification(
                    "Transferência realizada",
                    $"Valor: {amount} para conta {targetAccount.Number}",
                    NotificationType.Transaction,
                    sourceAccount.CustomerId));

            return _mapper.Map<TransactionResponse>(sourceAccount.Transactions.Last());
        }
    }
}