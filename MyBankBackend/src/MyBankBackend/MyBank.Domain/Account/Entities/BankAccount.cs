using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBank.Domain.Account.Entities
{
    public class BankAccount
    {
        public string Id { get; private set; }
        public string AccountNumber { get; private set; }
        public string Agency { get; private set; }
        public decimal Balance { get; private set; }
        public string OwnerId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public bool IsActive { get; private set; }

        public BankAccount(string accountNumber, string agency, string ownerId)
        {
            Id = Guid.NewGuid().ToString();
            AccountNumber = accountNumber;
            Agency = agency;
            Balance = 0;
            OwnerId = ownerId;
            CreatedAt = DateTime.UtcNow;
            IsActive = true;
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("O valor do depósito deve ser positivo");

            Balance += amount;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Withdraw(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("O valor do saque deve ser positivo");

            if (Balance < amount)
                throw new InvalidOperationException("Saldo insuficiente");

            Balance -= amount;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Deactivate()
        {
            IsActive = false;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
