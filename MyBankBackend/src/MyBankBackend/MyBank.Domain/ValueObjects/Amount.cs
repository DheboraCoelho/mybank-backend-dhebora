// Domain/ValueObjects/Amount.cs
using MyBank.Domain.Enums;
using MyBank.Domain.Exceptions;

// Domain/ValueObjects/Amount.cs
using MyBank.Domain.Enums;
using MyBank.Domain.Exceptions;

namespace MyBank.Domain.ValueObjects
{
    public record Amount
    {
        public decimal Value { get; }
        public CurrencyType Currency { get; }

        // Construtor principal
        public Amount(decimal value, CurrencyType currency)
        {
            if (value < 0)
                throw new DomainException("O valor não pode ser negativo");

            if (decimal.Round(value, 2) != value)
                throw new DomainException("O valor deve ter no máximo 2 casas decimais");

            Value = value;
            Currency = currency;
        }

        // Métodos de negócio
        public static Amount Zero(CurrencyType currency) => new(0m, currency);

        public bool IsZero() => Value == 0m;

        // Sobrecarga de operadores
        public static Amount operator +(Amount a, Amount b)
        {
            ValidateSameCurrency(a, b);
            return new Amount(a.Value + b.Value, a.Currency);
        }

        public static Amount operator -(Amount a, Amount b)
        {
            ValidateSameCurrency(a, b);
            return new Amount(a.Value - b.Value, a.Currency);
        }

        public static bool operator >(Amount a, Amount b)
        {
            ValidateSameCurrency(a, b);
            return a.Value > b.Value;
        }

        public static bool operator <(Amount a, Amount b)
        {
            ValidateSameCurrency(a, b);
            return a.Value < b.Value;
        }

        // Validação privada
        private static void ValidateSameCurrency(Amount a, Amount b)
        {
            if (a.Currency != b.Currency)
                throw new DomainException("Não é possível operar com valores de moedas diferentes");
        }

        // Formatação
        public override string ToString()
        {
            return Currency switch
            {
                CurrencyType.BRL => $"R$ {Value:N2}",
                CurrencyType.USD => $"US$ {Value:N2}",
                CurrencyType.EUR => $"€ {Value:N2}",
                _ => $"{Value:N2} {Currency}"
            };
        }
    }
}