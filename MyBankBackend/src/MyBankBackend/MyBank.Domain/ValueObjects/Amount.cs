// Domain/ValueObjects/Amount.cs
using MyBank.Domain.Enums;
using MyBank.Domain.Exceptions;

namespace MyBank.Domain.ValueObjects;

public record Amount
{
    public decimal Value { get; }
    public CurrencyType Currency { get; }

    public Amount(decimal value, CurrencyType currency)
    {
        if (value < 0)
            throw new DomainException("Valor não pode ser negativo");

        Value = value;
        Currency = currency;
    }

    // Sobrecarga de operadores para facilitar operações matemáticas
    public static Amount operator +(Amount a, Amount b)
    {
        if (a.Currency != b.Currency)
            throw new DomainException("Moedas diferentes não podem ser somadas");

        return new Amount(a.Value + b.Value, a.Currency);
    }

    public static Amount operator -(Amount a, Amount b)
    {
        if (a.Currency != b.Currency)
            throw new DomainException("Moedas diferentes não podem ser subtraídas");

        return new Amount(a.Value - b.Value, a.Currency);
    }
}