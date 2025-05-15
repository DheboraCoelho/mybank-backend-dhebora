using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Domain/Entities/Transaction.cs
using MyBank.Domain.Enums;

namespace MyBank.Domain.Entities
{
    public class Transaction : EntityBase
    {
        public decimal Amount { get; private set; }
        public TransactionType Type { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public Guid AccountId { get; private set; }

        public Transaction(decimal amount, TransactionType type)
        {
            Amount = amount;
            Type = type;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
