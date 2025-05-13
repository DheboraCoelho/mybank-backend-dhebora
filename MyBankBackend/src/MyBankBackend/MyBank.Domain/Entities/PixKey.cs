
using MyBank.Domain.Enums;

namespace MyBank.Domain.Entities
{
    public class PixKey : EntityBase
    {
        public string Key { get; private set; }
        public PixKeyType Type { get; private set; }
        public Guid AccountId { get; private set; }

        protected PixKey() { }

        public PixKey(string key, PixKeyType type, Guid accountId)
        {
            Key = key;
            Type = type;
            AccountId = accountId;
        }
    }
}