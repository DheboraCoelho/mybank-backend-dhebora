using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyBank.Domain.Enums;

namespace MyBank.Domain.Entities
{
    public class Notification : EntityBase
    {
        public string Title { get; private set; }
        public string Message { get; private set; }
        public bool IsRead { get; private set; }
        public NotificationType Type { get; private set; }
        public Guid UserId { get; private set; }

        protected Notification() { }

        public Notification(string title, string message, NotificationType type, Guid userId)
        {
            Title = title;
            Message = message;
            Type = type;
            UserId = userId;
            IsRead = false;
        }

        public void MarkAsRead()
        {
            if (!IsRead)
            {
                IsRead = true;
                UpdateTimestamps();
            }
        }
    }
}
