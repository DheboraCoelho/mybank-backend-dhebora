using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Application/DTOs/Accounts/TransactionResponse.cs
namespace MyBank.Application.DTOs.Accounts
{
    public class TransactionResponse
    {
        public Guid TransactionId { get; set; }
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; } // "Deposit", "Withdrawal", "Transfer"
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }
}