using System;

namespace MyBank.Domain.Pix.Entities
{
    public class PixTransaction
    {
        public string Id { get; private set; }
        public string SenderAccountId { get; private set; }
        public string ReceiverKey { get; private set; } // Chave PIX (CPF, email, telefone, etc.)
        public decimal Amount { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public string Status { get; private set; } // "Pending", "Completed", "Failed"

        public PixTransaction(string senderAccountId, string receiverKey, decimal amount)
        {
            Id = Guid.NewGuid().ToString();
            SenderAccountId = senderAccountId ?? throw new ArgumentNullException(nameof(senderAccountId));
            ReceiverKey = receiverKey ?? throw new ArgumentNullException(nameof(receiverKey));
            Amount = amount;
            CreatedAt = DateTime.UtcNow;
            Status = "Pending";
        }

        public void Complete()
        {
            Status = "Completed";
        }

        public void Fail()
        {
            Status = "Failed";
        }
    }
}
