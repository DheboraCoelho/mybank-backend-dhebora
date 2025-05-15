using MyBank.Domain.Enums;
using System;
using System.Collections.Generic;

namespace MyBank.Domain.Entities
{
    public class Account
    {
        public Guid Id { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public Guid CustomerId { get; set; }
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();

        public void Deposit(decimal amount)
        {
            if (amount <= 0) throw new ArgumentException("Valor deve ser positivo");
            Balance += amount;
            Transactions.Add(new Transaction(amount, TransactionType.Deposit));
        }

        public void Withdraw(decimal amount)
        {
            if (amount <= 0) throw new ArgumentException("Valor deve ser positivo");
            if (Balance < amount) throw new InvalidOperationException("Saldo insuficiente");
            Balance -= amount;
            Transactions.Add(new Transaction(amount, TransactionType.Withdrawal));
        }

        public void Transfer(Account destination, decimal amount)
        {
            Withdraw(amount);
            destination.Deposit(amount);
            Transactions.Add(new Transaction(amount, TransactionType.Transfer));
        }
    }
}