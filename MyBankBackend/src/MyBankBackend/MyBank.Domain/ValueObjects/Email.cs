using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Domain/ValueObjects/Email.cs
using System.Text.RegularExpressions;
using MyBank.Domain.Exceptions;

namespace MyBank.Domain.ValueObjects
{
    public record Email
    {
        public string Value { get; }
        public string LocalPart => Value.Split('@')[0];
        public string Domain => Value.Split('@')[1];

        private const string EmailRegexPattern =
            @"^[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z]{2,}$";

        private static readonly Regex EmailRegex =
            new Regex(EmailRegexPattern, RegexOptions.Compiled);

        public Email(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("Email não pode ser vazio");

            value = value.Trim().ToLowerInvariant();

            if (!EmailRegex.IsMatch(value))
                throw new DomainException("Email inválido");

            if (value.Length > 254)
                throw new DomainException("Email excede o tamanho máximo");

            Value = value;
        }

        public static bool IsValid(string email) =>
            !string.IsNullOrWhiteSpace(email) &&
            EmailRegex.IsMatch(email);

        public override string ToString() => Value;

        // Conversão implícita para string
        public static implicit operator string(Email email) => email.Value;

        // Operador de igualdade adicional para strings
        public bool Equals(string otherEmail) =>
            !string.IsNullOrEmpty(otherEmail) &&
            Value.Equals(otherEmail.Trim().ToLowerInvariant());
    }
}
