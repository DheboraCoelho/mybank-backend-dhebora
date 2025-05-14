// Domain/Entities/Account.cs
using MyBank.Domain.Enums;
using MyBank.Domain.Exceptions;
using MyBank.Domain.ValueObjects;

namespace MyBank.Domain.Entities
{
    public class Account : EntityBase
    {
        public string Number { get; private set; }
        public Amount Balance { get; private set; }
        public Guid CustomerId { get; private set; }
        public List<Transaction> Transactions { get; private set; } = new();
        public List<PixKey> PixKeys { get; private set; } = new();

        protected Account() { }

        public Account(Guid customerId, string accountNumber, Amount? initialBalance = null)
        {
            Id = Guid.NewGuid();
            CustomerId = customerId;
            Number = accountNumber;
            Balance = initialBalance ?? new Amount(0, CurrencyType.BRL);
            CreatedAt = DateTime.UtcNow;
        }

        public void Deposit(Amount amount, string? description = null)
        {
            if (amount.Value <= 0)
                throw new DomainException("Valor do depósito deve ser positivo");

            Balance = new Amount(Balance.Value + amount.Value, Balance.Currency);
            Transactions.Add(new Transaction(amount.Value, TransactionType.Deposit, Id, description));
        }

        public void Withdraw(Amount amount, string? description = null)
        {
            if (amount.Value <= 0)
                throw new DomainException("Valor do saque deve ser positivo");

            if (Balance.Value < amount.Value)
                throw new InsufficientBalanceException();

            Balance = new Amount(Balance.Value - amount.Value, Balance.Currency);
            Transactions.Add(new Transaction(amount.Value, TransactionType.Withdraw, Id, description));
        }

        public void TransferTo(Account destinationAccount, Amount amount, string? description = null)
        {
            if (amount.Value <= 0)
                throw new DomainException("Valor da transferência deve ser positivo");

            if (Balance.Value < amount.Value)
                throw new InsufficientBalanceException();

            if (Balance.Currency != destinationAccount.Balance.Currency)
                throw new DomainException("As contas devem ter a mesma moeda para transferência");

            Balance = new Amount(Balance.Value - amount.Value, Balance.Currency);
            destinationAccount.Balance = new Amount(destinationAccount.Balance.Value + amount.Value, destinationAccount.Balance.Currency);

            Transactions.Add(new Transaction(amount.Value, TransactionType.Transfer, Id, description));
            destinationAccount.Transactions.Add(new Transaction(amount.Value, TransactionType.Transfer, destinationAccount.Id, description));
        }

        public void AddPixKey(PixKey pixKey)
        {
            if (PixKeys.Any(k => k.Key == pixKey.Key))
                throw new DomainException("Chave PIX já cadastrada");

            PixKeys.Add(pixKey);
        }
    }
}
