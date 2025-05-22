using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBank.Domain.Account.ValueObjects.Shared
{
    public sealed record Email
    {
        public string Value { get; }

        private Email(string value) => Value = value;

        public static Email Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Email não pode ser vazio");

            try
            {
                var mailAddress = new System.Net.Mail.MailAddress(value);
                if (mailAddress.Address != value)
                    throw new ArgumentException("Email inválido");
            }
            catch
            {
                throw new ArgumentException("Email inválido");
            }

            return new Email(value);
        }
    }
}
