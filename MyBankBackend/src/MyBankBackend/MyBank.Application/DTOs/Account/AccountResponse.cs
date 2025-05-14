using MyBank.Application.DTOs.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Application/DTOs/Account/AccountResponse.cs
// Application/DTOs/Account/AccountResponse.cs
namespace MyBank.Application.DTOs.Accounts
{
    public record AccountResponse(
        Guid Id,
        string Number,
        decimal Balance,
        string Currency,
        Guid CustomerId,
        DateTime CreatedAt,
        IEnumerable<TransactionResponse> Transactions);
}
