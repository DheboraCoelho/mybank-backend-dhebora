using MyBank.Domain.Account.ValueObjects;
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
        public AccountNumber Number { get; private set; }
        public Agency Agency { get; private set; }
        public Money Balance { get; private set; }
        public string OwnerId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public bool IsActive { get; private set; }

        public BankAccount(AccountNumber number, Agency agency, string ownerId)
        {
            Id = Guid.NewGuid().ToString();
            Number = number ?? throw new ArgumentNullException(nameof(number));
            Agency = agency ?? throw new ArgumentNullException(nameof(agency));
            Balance = Money.Create(0);
            OwnerId = ownerId ?? throw new ArgumentNullException(nameof(ownerId));
            CreatedAt = DateTime.UtcNow;
            IsActive = true;
        }

        public void Deposit(Money amount)
        {
            if (amount == null)
                throw new ArgumentNullException(nameof(amount));
            if (amount.Amount <= 0)
                throw new ArgumentException("O valor do depósito deve ser positivo");

            Balance = Money.Create(Balance.Amount + amount.Amount);
            UpdatedAt = DateTime.UtcNow;
        }

        public void Withdraw(Money amount)
        {
            if (amount == null)
                throw new ArgumentNullException(nameof(amount));
            if (amount.Amount <= 0)
                throw new ArgumentException("O valor do saque deve ser positivo");
            if (Balance.Amount < amount.Amount)
                throw new InvalidOperationException("Saldo insuficiente");

            Balance = Money.Create(Balance.Amount - amount.Amount);
            UpdatedAt = DateTime.UtcNow;
        }

        public void Deactivate()
        {
            if (!IsActive)
                throw new InvalidOperationException("Conta já está desativada");

            IsActive = false;
            UpdatedAt = DateTime.UtcNow;
        }

        // Método adicional para reativar conta
        public void Reactivate()
        {
            if (IsActive)
                throw new InvalidOperationException("Conta já está ativa");

            IsActive = true;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
