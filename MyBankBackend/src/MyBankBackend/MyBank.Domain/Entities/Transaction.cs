using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MyBank.Domain.Enums;

namespace MyBank.Domain.Entities
{
    public class Transaction : EntityBase
    {
        public decimal Amount { get; private set; }
        public TransactionType Type { get; private set; }
        public Guid AccountId { get; private set; }
        public DateTime TransactionDate { get; private set; }

        protected Transaction() { }

        public Transaction(decimal amount, TransactionType type, Guid accountId)
        {
            Amount = amount;
            Type = type;
            AccountId = accountId;
            TransactionDate = DateTime.UtcNow;
        }
    }
}
