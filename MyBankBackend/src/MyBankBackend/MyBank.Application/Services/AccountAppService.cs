using AutoMapper;
using MyBank.Application.DTOs.Accounts;
using MyBank.Application.Interfaces;
using MyBank.Domain.Entities;
using MyBank.Domain.Enums;
using MyBank.Domain.Exceptions;
using MyBank.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBank.Application.Services
{
    public class AccountAppService 
    {
        private readonly IAccountRepository _accountRepository;
        //private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AccountAppService(
            IAccountRepository accountRepository,
          //  ITransactionRepository transactionRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _accountRepository = accountRepository;
           // _transactionRepository = transactionRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<AccountResponse> GetAccountByIdAsync(Guid accountId)
        {
            var account = await _accountRepository.GetByIdWithTransactionsAsync(accountId);
            if (account == null)
                throw new DomainException("Conta não encontrada");

            return _mapper.Map<AccountResponse>(account);
        }

       

        
        public async Task<decimal> GetCurrentBalanceAsync(Guid accountId)
        {
            var account = await _accountRepository.GetByIdAsync(accountId);
            if (account == null)
                throw new DomainException("Conta não encontrada");

            return account.Balance;
        }

        public async Task<PixKeyResponse> RegisterPixKeyAsync(RegisterPixKeyRequest request)
        {
            var account = await _accountRepository.GetByIdAsync(request.AccountId);
            if (account == null)
                throw new DomainException("Conta não encontrada");

            var pixKey = new PixKey(request.Key, request.Type, account.Id);
          //  account.PixKeys.Add(pixKey);

            await _accountRepository.UpdateAsync(account);
            return _mapper.Map<PixKeyResponse>(pixKey);
        }
    }
}