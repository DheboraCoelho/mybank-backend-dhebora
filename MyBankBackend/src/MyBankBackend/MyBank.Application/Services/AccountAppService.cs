// Application/Services/AccountAppService.cs
using MyBank.Application.Interfaces;
using MyBank.Application.DTOs.Accounts;
using MyBank.Domain.Interfaces;
using MyBank.Domain.Entities;
using MyBank.Domain.ValueObjects;
using AutoMapper;
using System.Transactions;

namespace MyBank.Application.Services
{
    public class AccountAppService : IAccountAppService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;

        public AccountAppService(
            IAccountRepository accountRepository,
            INotificationService notificationService,
            IMapper mapper)
        {
            _accountRepository = accountRepository;
            _notificationService = notificationService;
            _mapper = mapper;
        }

        // Implementação dos métodos da interface...
        public async Task<TransactionResponse> TransferAsync(TransferRequest request)
        {
            var sourceAccount = await _accountRepository.GetByIdAsync(request.SourceAccountId);
            var targetAccount = await _accountRepository.GetByIdAsync(request.TargetAccountId);

            // ... lógica de transferência

            await _notificationService.SendAsync(new TransferNotification(
                sourceAccount.Id,
                targetAccount.Id,
                request.Amount
            ));

            return _mapper.Map<TransactionResponse>(transaction);
        }
    }
}