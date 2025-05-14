using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Application/DTOs/Accounts/TransferRequest.cs
namespace MyBank.Application.DTOs.Accounts
{
    public record TransferRequest(
        Guid SourceAccountId,
        Guid TargetAccountId,
        decimal Amount,
        string Description );
}
