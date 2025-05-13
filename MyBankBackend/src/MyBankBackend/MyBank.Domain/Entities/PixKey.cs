
using MyBank.Domain.Enums;
using MyBank.Domain.Exceptions;

namespace MyBank.Domain.Entities
{
    // Domain/Entities/PixKey.cs
    public class PixKey : EntityBase
    {
        public string Key { get; private set; }
        public PixKeyType Type { get; private set; }
        public Guid AccountId { get; private set; }

        // Construtor completo
        public PixKey(string key, PixKeyType type, Guid accountId)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new DomainException("Chave PIX não pode ser vazia");

            Key = key;
            Type = type;
            AccountId = accountId;
        }

        // Construtor vazio para o EF Core
        protected PixKey() { }
    }
}
