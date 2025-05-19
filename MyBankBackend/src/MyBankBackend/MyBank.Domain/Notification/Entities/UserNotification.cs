using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBank.Domain.Notification.Entities
{
    public class UserNotification  
    {
        public UserNotification(string userId, string title, string message)
        {
            Id = Guid.NewGuid().ToString();
            UserId = userId;
            Title = title;
            Message = message;
            CreatedAt = DateTime.UtcNow;
            IsRead = false;
        }
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsRead { get; set; }
        public void MarkAsRead()
        {
            IsRead = true;
        }
    }
}
