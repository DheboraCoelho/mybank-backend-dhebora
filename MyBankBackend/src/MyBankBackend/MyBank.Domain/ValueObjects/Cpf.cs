
using MyBank.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MyBank.Core.ValueObjects
{
    public record Cpf  // Note o 'public' aqui
    {
        public string Value { get; }

        public Cpf(string value)
        {
            if (!IsValid(value))
                throw new DomainException("CPF inválido");

            Value = value;
        }

        private static bool IsValid(string cpf)
        {
            // Implementação da validação de CPF
            return true;
        }
    }
}