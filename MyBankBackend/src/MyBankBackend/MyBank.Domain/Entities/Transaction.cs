using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Domain/Entities/Transaction.cs
using MyBank.Domain.Enums;

namespace MyBank.Domain.Entities
{
    public class Transaction
    {
        public Guid Id { get; private set; }
        public decimal Amount { get; private set; }
        public TransactionType Type { get; private set; }
        public Guid AccountId { get; private set; }
        public string? Description { get; private set; }
        public DateTime Date { get; private set; }

        public Transaction(decimal amount, TransactionType type, Guid accountId, string? description = null)
        {
            Id = Guid.NewGuid();
            Amount = amount;
            Type = type;
            AccountId = accountId;
            Description = description;
            Date = DateTime.UtcNow;
        }
    }
}
