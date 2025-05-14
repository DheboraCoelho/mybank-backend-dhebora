using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBank.Application.DTOs.Accounts
{
    public record WithdrawRequest(
        Guid AccountId,
        decimal Amount,
        string Description = "");
}