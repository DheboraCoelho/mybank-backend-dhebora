using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyBank.Domain.Account.ValueObjects
{
    public sealed class AccountNumber
    {
        public string Value { get; }

        private AccountNumber(string value) => Value = value;

        public static AccountNumber Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Número da conta não pode ser vazio");

            if (!Regex.IsMatch(value, @"^\d{5}-\d{1}$"))
                throw new ArgumentException("Formato inválido. Use XXXXX-X");

            return new AccountNumber(value);
        }

        public override string ToString() => Value;
    }
}

