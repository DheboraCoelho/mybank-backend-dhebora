// Domain/Entities/Account.cs
using MyBank.Domain.Enums;
using MyBank.Domain.Exceptions;
using MyBank.Domain.ValueObjects;


using MyBank.Domain.Entities;

using System.Transactions;

namespace MyBank.Domain.Entities;

public class Account : EntityBase
{
    // Propriedades
    public string Number { get; private set; }
    public Amount Balance { get; private set; }
    public Guid CustomerId { get; private set; }
    public List<Transaction> Transactions { get; private set; } = new();
    public List<PixKey> PixKeys { get; private set; } = new();

    // Construtor
    protected Account() { } // Para ORM

    public Account(Guid customerId, string accountNumber, Amount initialBalance = null)
    {
        Id = Guid.NewGuid();
        CustomerId = customerId;
        Number = accountNumber;
        Balance = initialBalance ?? new Amount(0, CurrencyType.BRL);
        CreatedAt = DateTime.UtcNow;
    }

    // Métodos de Domínio
    public void Deposit(Amount amount)
    {
        if (amount.Value <= 0)
            throw new DomainException("Valor do depósito deve ser positivo");

        Balance = new Amount(Balance.Value + amount.Value, Balance.Currency);
        Transactions.Add(new Transaction(amount.Value, TransactionType.Deposit, Id));
    }

    public void Withdraw(Amount amount)
    {
        if (amount.Value <= 0)
            throw new DomainException("Valor do saque deve ser positivo");

        if (Balance.Value < amount.Value)
            throw new InsufficientBalanceException();

        Balance = new Amount(Balance.Value - amount.Value, Balance.Currency);
        Transactions.Add(new Transaction(amount.Value, TransactionType.Withdraw, Id));
    }

    public void AddPixKey(PixKey pixKey)
    {
        if (PixKeys.Any(k => k.Key == pixKey.Key))
            throw new DomainException("Chave PIX já cadastrada");

        PixKeys.Add(pixKey);
    }
}