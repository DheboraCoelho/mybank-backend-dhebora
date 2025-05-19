using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyBank.Domain.Pix.Entities;
using MyBank.Domain.Pix.Interfaces;
using MyBank.Application.DTOs;
using MyBank.Application.Interfaces;
using MyBank.Domain.Account.Interfaces;

namespace MyBank.Application.Services
{
    public class PixService : IPixService
    {
        private readonly IPixRepository _pixRepository;
        private readonly IAccountRepository _accountRepository;

        public PixService(IPixRepository pixRepository, IAccountRepository accountRepository)
        {
            _pixRepository = pixRepository;
            _accountRepository = accountRepository;
        }

        public async Task<PixResponse> CreatePixTransaction(PixRequest request)
        {
            var senderAccount = await _accountRepository.GetByIdAsync(request.SenderAccountId);
            if (senderAccount == null)
                throw new ArgumentException("Conta do remetente não encontrada");

            if (senderAccount.Balance < request.Amount)
                throw new InvalidOperationException("Saldo insuficiente");

            var pixTransaction = new PixTransaction(
                request.SenderAccountId,
                request.ReceiverKey,
                request.Amount);

            await _pixRepository.AddAsync(pixTransaction);

            try
            {
                senderAccount.Withdraw(request.Amount);
                await _accountRepository.UpdateAsync(senderAccount);

                pixTransaction.Complete();
            }
            catch
            {
                pixTransaction.Fail();
                throw;
            }
            finally
            {
                await _pixRepository.UpdateAsync(pixTransaction);
            }

            return new PixResponse
            {
                Id = pixTransaction.Id,
                Status = pixTransaction.Status,
                Amount = pixTransaction.Amount,
                CreatedAt = pixTransaction.CreatedAt
            };
        }

        public async Task<PixResponse> GetPixTransaction(string id)
        {
            var transaction = await _pixRepository.GetByIdAsync(id);
            if (transaction == null) return null;

            return new PixResponse
            {
                Id = transaction.Id,
                Status = transaction.Status,
                Amount = transaction.Amount,
                CreatedAt = transaction.CreatedAt
            };
        }
    }
}

