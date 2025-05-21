using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBank.Domain.Account.ValueObjects
{
    public sealed class Money
    {
        public decimal Amount { get; }
        public Currency Currency { get; } = Currency.BRL;

        private Money(decimal amount) => Amount = amount;

        public static Money Create(decimal amount)
        {
            if (amount < 0)
                throw new ArgumentException("Valor não pode ser negativo");

            if (decimal.Round(amount, 2) != amount)
                throw new ArgumentException("Deve ter no máximo 2 casas decimais");

            return new Money(amount);
        }

        public static Money operator +(Money a, Money b)
            => Create(a.Amount + b.Amount);

        public static Money operator -(Money a, Money b)
            => a.Amount >= b.Amount
                ? Create(a.Amount - b.Amount)
                : throw new InvalidOperationException("Saldo insuficiente");

        public override string ToString()
            => $"{Amount.ToString("N2")} {Currency}";
    }

    public enum Currency { BRL, USD, EUR }
}
