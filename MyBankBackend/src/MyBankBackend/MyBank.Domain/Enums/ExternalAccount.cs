using MyBank.Core.ValueObjects;
using MyBank.Domain.Enums;
using MyBank.Domain.Exceptions;
using MyBank.Domain.ValueObjects;

namespace MyBank.Domain.Entities
{
    public class ExternalAccount : EntityBase
    {
        // Propriedades
        public int BankNumber { get; private set; }
        public int AgencyNumber { get; private set; }
        public string AccountNumber { get; private set; }
        public Cpf Cpf { get; private set; } // Usando o Value Object Cpf
        public List<PixKey> PixKeys { get; private set; } = new();

        // Construtores
        protected ExternalAccount() { } // Para ORM

        public ExternalAccount(int bankNumber, int agencyNumber, string accountNumber, Cpf cpf)
        {
            Validate(bankNumber, agencyNumber, accountNumber);

            BankNumber = bankNumber;
            AgencyNumber = agencyNumber;
            AccountNumber = accountNumber;
            Cpf = cpf;
        }

        // Métodos de Domínio
        public void AddPixKey(string key, PixKeyType type)
        {
            if (PixKeys.Any(k => k.Key == key))
                throw new DomainException("Chave PIX já cadastrada para esta conta");

            PixKeys.Add(new PixKey(key, type, Id)); // Associa com o Id da ExternalAccount
        }

        public void UpdateAccountInfo(int bankNumber, int agencyNumber, string accountNumber)
        {
            Validate(bankNumber, agencyNumber, accountNumber);

            BankNumber = bankNumber;
            AgencyNumber = agencyNumber;
            AccountNumber = accountNumber;
            UpdateTimestamps();
        }

        // Validações privadas
        private void Validate(int bankNumber, int agencyNumber, string accountNumber)
        {
            if (bankNumber <= 0)
                throw new DomainException("Número do banco inválido");

            if (agencyNumber <= 0)
                throw new DomainException("Número da agência inválido");

            if (string.IsNullOrWhiteSpace(accountNumber))
                throw new DomainException("Número da conta inválido");
        }
    }
}