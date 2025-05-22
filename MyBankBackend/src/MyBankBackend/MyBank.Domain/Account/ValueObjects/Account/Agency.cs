using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// MyBank.Domain.Account.ValueObjects/Agency.cs
using System;
using System.Text.RegularExpressions;

namespace MyBank.Domain.Account.ValueObjects.Account
{
    public sealed class Agency
    {
        public string Value { get; }

        private Agency(string value) => Value = value;

        public static Agency Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Agência não pode ser vazia");

            if (!Regex.IsMatch(value, @"^\d{4}$"))
                throw new ArgumentException("Agência deve ter 4 dígitos");

            return new Agency(value);
        }

        public override string ToString() => Value;
    }
}