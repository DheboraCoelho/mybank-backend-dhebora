using MyBank.Domain.Enums;

// Domain/Entities/BankTransaction.cs

using MyBank.Domain.Enums;
using MyBank.Domain.Exceptions;

namespace MyBank.Domain.Entities
{
    public class BankTransaction : EntityBase
    {
        public decimal Amount { get; private set; }
        public TransactionType Type { get; private set; }
        public Guid AccountId { get; private set; }
        public DateTime TransactionDate { get; private set; }

        // Construtor para ORM
        protected BankTransaction() { }

        public BankTransaction(decimal amount, TransactionType type, Guid accountId)
        {
            if (amount <= 0)
                throw new DomainException("Valor da transação deve ser positivo");

            Amount = amount;
            Type = type;
            AccountId = accountId;
            TransactionDate = DateTime.UtcNow;
        }
    }
}