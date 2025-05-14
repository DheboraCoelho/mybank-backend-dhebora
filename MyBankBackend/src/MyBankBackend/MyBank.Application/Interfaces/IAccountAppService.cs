using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Application/Interfaces/IAccountAppService.cs
using MyBank.Application.DTOs.Accounts;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyBank.Application.DTOs.Shared.MyBank.Application.DTOs.Shared;


using System.Collections.Generic;
using System.Threading.Tasks;


namespace MyBank.Application.Interfaces
{
    public interface IAccountAppService
    {
        Task<AccountResponse> GetAccountByIdAsync(Guid accountId);
        Task<TransactionResponse> DepositAsync(TransactionRequest request);
        Task<TransactionResponse> WithdrawAsync(WithdrawRequest request);
        Task<TransactionResponse> TransferAsync(TransferRequest request);
        Task<IEnumerable<TransactionResponse>> GetStatementAsync(Guid accountId, DateRangeRequest period);
        Task<decimal> GetCurrentBalanceAsync(Guid accountId);
    }
}