using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Application/DTOs/Account/TransactionRequest.cs
namespace MyBank.Application.DTOs.Accounts
{
    public record TransactionRequest(
        Guid AccountId,
        decimal Amount,
        string Description);
}
