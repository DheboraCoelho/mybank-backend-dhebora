using AutoMapper;
using MyBank.Domain.Entities;
using MyBank.Domain.Interfaces;
using MyBank.Application.DTOs.Accounts;
using MyBank.Domain.Exceptions;
using MyBank.Domain.ValueObjects;
using MyBank.Domain.Enums;
using MyBank.Application.Interfaces;
using MyBank.Core.ValueObjects;

namespace MyBank.Application.Services
{
    public class AccountAppService : IAccountAppService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;

        public AccountAppService(
            IAccountRepository accountRepository,
            ICustomerRepository customerRepository,
            INotificationService notificationService,
            IMapper mapper)
        {
            _accountRepository = accountRepository;
            _customerRepository = customerRepository;
            _notificationService = notificationService;
            _mapper = mapper;
        }

        public async Task<AccountResponse> CreateAccountAsync(CreateAccountRequest request)
        {
            var customer = await _customerRepository.GetByCpfAsync(new Cpf(request.Cpf));
            if (customer == null)
                throw new DomainException("Cliente não encontrado");

            var account = new Account(
                customer.Id,
                request.AccountType,
                new Amount(request.InitialBalance, CurrencyType.BRL));

            await _accountRepository.AddAsync(account);

            await _notificationService.SendAsync(
                new Notification(
                    "Conta criada",
                    $"Sua conta {account.Number} foi aberta com sucesso",
                    NotificationType.Account,
                    customer.Id));

            return _mapper.Map<AccountResponse>(account);
        }

        public async Task<TransactionResponse> TransferAsync(TransferRequest request)
        {
            var sourceAccount = await _accountRepository.GetByIdAsync(request.SourceAccountId);
            var targetAccount = await _accountRepository.GetByNumberAsync(request.TargetAccountNumber);

            // Validações
            if (sourceAccount == null || targetAccount == null)
                throw new DomainException("Conta origem ou destino inválida");

            var amount = new Amount(request.Amount, CurrencyType.BRL);
            sourceAccount.Transfer(amount, targetAccount);

            await _accountRepository.UpdateAsync(sourceAccount);
            await _accountRepository.UpdateAsync(targetAccount);

            // Notificações
            await SendTransferNotifications(sourceAccount, targetAccount, amount);

            return _mapper.Map<TransactionResponse>(sourceAccount.Transactions.Last());
        }

        private async Task SendTransferNotifications(Account source, Account target, Amount amount)
        {
            var notificationSource = new Notification(
                "Transferência realizada",
                $"Você transferiu {amount} para conta {target.Number}",
                NotificationType.Transaction,
                source.CustomerId);

            var notificationTarget = new Notification(
                "Transferência recebida",
                $"Você recebeu {amount} da conta {source.Number}",
                NotificationType.Transaction,
                target.CustomerId);

            await _notificationService.SendAsync(notificationSource);
            await _notificationService.SendAsync(notificationTarget);
        }
    }
}