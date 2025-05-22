using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBank.Domain.Account.ValueObjects.Shared
{
    public sealed record TransactionDescription
    {
        public string Value { get; }

        private TransactionDescription(string value) => Value = value;

        public static TransactionDescription Create(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Descrição não pode ser vazia");

            if (description.Length > 100)
                throw new ArgumentException("Máximo 100 caracteres");

            return new TransactionDescription(description.Trim());
        }

        public static TransactionDescription DefaultFor(string transactionType)
            => new($"Transação {transactionType} em {DateTime.Now:dd/MM}");
    }
}
