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
    public class AccountResponse
    {
        public Guid Id { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public List<TransactionResponse> Transactions { get; set; }
        public List<PixKeyResponse> PixKeys { get; set; }
    }

   

    public class PixKeyResponse
    {
        public string Key { get; set; }
        public string Type { get; set; }
    }
}
