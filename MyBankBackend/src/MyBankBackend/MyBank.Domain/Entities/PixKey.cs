
using MyBank.Domain.Enums;
using MyBank.Domain.Exceptions;

namespace MyBank.Domain.Entities
{
    public class PixKey
    {
        public Guid Id { get; private set; }
        public string Key { get; private set; }
        public PixKeyType Type { get; private set; }
        public Guid AccountId { get; private set; }

        public PixKey(string key, PixKeyType type, Guid accountId)
        {
            Key = key;
            Type = type;
            AccountId = accountId;
        }
    }
}
