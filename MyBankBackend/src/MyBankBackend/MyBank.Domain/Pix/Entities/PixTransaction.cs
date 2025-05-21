using System;

namespace MyBank.Domain.Pix.Entities
{
    public class PixTransaction
    {
      
            public string Id { get; private set; }
            public string SenderAccountId { get; private set; }
            public string ReceiverKey { get; private set; }
            public decimal Amount { get; private set; }
            public DateTime CreatedAt { get; private set; }
            public string Status { get; private set; } // "Pending", "Completed", "Failed"

            public PixTransaction(string senderAccountId, string receiverKey, decimal amount)
            {
                Id = Guid.NewGuid().ToString();
                SenderAccountId = senderAccountId;
                ReceiverKey = receiverKey;
                Amount = amount;
                CreatedAt = DateTime.UtcNow;
                Status = "Pending";
            }

            public void Complete() => Status = "Completed";
            public void Fail() => Status = "Failed";
        }
    }
    
