using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBank.Application.DTOs
{
    public class CreateAccountRequest
    {
        public string OwnerId { get; set; }
        public string AccountNumber { get; set; }
        public string Agency { get; set; }
    }

    public class AccountResponse
    {
        public string Id { get; set; }
        public string AccountNumber { get; set; }
        public string Agency { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
    }

    public class TransactionRequest
    {
        public decimal Amount { get; set; }
    }
}
