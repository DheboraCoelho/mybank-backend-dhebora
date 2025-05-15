using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Application/Interfaces/INotificationAppService.cs
using MyBank.Application.DTOs.Notifications;
using System.Collections.Generic;
using System.Threading.Tasks;

using MyBank.Application.DTOs.Notifications;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBank.Application.Interfaces
{
    public interface INotificationAppService
    {
        Task<NotificationResponse> SendNotificationAsync(CreateNotificationRequest request);
        Task<IEnumerable<NotificationResponse>> GetUserNotificationsAsync(Guid userId);
        Task MarkNotificationAsReadAsync(Guid notificationId);
        Task<int> GetUnreadCountAsync(Guid userId);
    }
}