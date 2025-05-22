using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using MyBank.Domain.Account.ValueObjects.Shared;

namespace MyBank.Domain.ValueObjects.Shared
{
    public sealed record Document
    {
        public string Value { get; }
        public DocumentType Type { get; }

        private Document(string value)
        {
            Value = value;
            Type = value.Length == 11 ? DocumentType.CPF : DocumentType.CNPJ;
        }

        public static Document Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Documento não pode ser vazio");

            // Remove formatação
            var cleanValue = Regex.Replace(value, @"[^\d]", "");

            if (!IsValid(cleanValue))
                throw new ArgumentException("Documento inválido");

            return new Document(cleanValue);
        }

        public static bool IsValid(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;

            var cleanValue = Regex.Replace(value, @"[^\d]", "");

            return cleanValue.Length switch
            {
                11 => ValidateCpf(cleanValue),
                14 => ValidateCnpj(cleanValue),
                _ => false
            };
        }

        private static bool ValidateCpf(string cpf)
        {
            // Todos dígitos iguais são inválidos
            if (cpf.Distinct().Count() == 1)
                return false;

            int[] multiplier1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplier2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf = cpf[..9];
            int sum = 0;

            for (int i = 0; i < 9; i++)
                sum += int.Parse(tempCpf[i].ToString()) * multiplier1[i];

            int remainder = sum % 11;
            remainder = remainder < 2 ? 0 : 11 - remainder;

            string digit = remainder.ToString();
            tempCpf += digit;
            sum = 0;

            for (int i = 0; i < 10; i++)
                sum += int.Parse(tempCpf[i].ToString()) * multiplier2[i];

            remainder = sum % 11;
            remainder = remainder < 2 ? 0 : 11 - remainder;

            digit += remainder.ToString();

            return cpf.EndsWith(digit);
        }

        private static bool ValidateCnpj(string cnpj)
        {
            // Todos dígitos iguais são inválidos
            if (cnpj.Distinct().Count() == 1)
                return false;

            int[] multiplier1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplier2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCnpj = cnpj[..12];
            int sum = 0;

            for (int i = 0; i < 12; i++)
                sum += int.Parse(tempCnpj[i].ToString()) * multiplier1[i];

            int remainder = sum % 11;
            remainder = remainder < 2 ? 0 : 11 - remainder;

            string digit = remainder.ToString();
            tempCnpj += digit;
            sum = 0;

            for (int i = 0; i < 13; i++)
                sum += int.Parse(tempCnpj[i].ToString()) * multiplier2[i];

            remainder = sum % 11;
            remainder = remainder < 2 ? 0 : 11 - remainder;

            digit += remainder.ToString();

            return cnpj.EndsWith(digit);
        }

        public string Formatted()
        {
            return Type switch
            {
                DocumentType.CPF => Convert.ToUInt64(Value).ToString(@"000\.000\.000\-00"),
                DocumentType.CNPJ => Convert.ToUInt64(Value).ToString(@"00\.000\.000\/0000\-00"),
                _ => Value
            };
        }

        public override string ToString() => Formatted();
    }

    public enum DocumentType
    {
        CPF,
        CNPJ
    }
}