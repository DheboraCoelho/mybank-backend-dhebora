using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Application/DTOs/Accounts/TransactionResponse.cs
namespace MyBank.Application.DTOs.Accounts
{
    public record TransactionResponse(
        Guid Id,
        decimal Amount,
        string Type,  // Será mapeado do TransactionType enum
        DateTime Date,
        string? Description = null);
}