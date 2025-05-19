using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

namespace MyBank.Application.DTOs
{
    public class PixRequest
    {
        public string SenderAccountId { get; set; }
        public string ReceiverKey { get; set; } // Chave PIX (CPF, email, telefone, etc.)
        public decimal Amount { get; set; }
    }

    public class PixResponse
    {
        public string Id { get; set; }
        public string Status { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
