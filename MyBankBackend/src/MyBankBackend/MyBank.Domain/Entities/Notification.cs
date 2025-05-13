using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBank.Domain.Entities
{
    public class Notification : EntityBase
    {
        public Guid UserId { get; private set; }
        public string Title { get; private set; }
        public string Message { get; private set; }
        public bool IsRead { get; private set; }
        public DateTime CreatedAt { get; private set; }

        // Métodos de domínio...
    }

    // Domain/Interfaces/INotificationGateway.cs
    public interface INotificationGateway
    {
        Task SendAsync(Notification notification);
        Task<IEnumerable<Notification>> GetByUserId(Guid userId);
    }
}
