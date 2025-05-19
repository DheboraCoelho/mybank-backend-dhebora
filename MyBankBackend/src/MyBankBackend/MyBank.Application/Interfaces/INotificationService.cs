using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using MyBank.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBank.Application.Interfaces
{
    public interface INotificationService
    {
        Task<NotificationResponse> CreateNotification(CreateNotificationRequest request);
        Task<IEnumerable<NotificationResponse>> GetUserNotifications(string userId);
        Task MarkAsRead(string notificationId);
    }
}
