using MyBank.Domain.Enums;
using MyBank.Domain.Exceptions;

namespace MyBank.Domain.Entities
{
    public class Account
    {
        public Guid Id { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

        public void Deposit(decimal amount)
        {
            if (amount <= 0) throw new ArgumentException("Amount must be positive");
            Balance += amount;
        }

        public void Withdraw(decimal amount)
        {
            if (amount <= 0) throw new ArgumentException("Amount must be positive");
            if (Balance < amount) throw new InsufficientBalanceException();
            Balance -= amount;
        }

        public void Transfer(Account destination, decimal amount)
        {
            this.Withdraw(amount);
            destination.Deposit(amount);
        }
    }
}