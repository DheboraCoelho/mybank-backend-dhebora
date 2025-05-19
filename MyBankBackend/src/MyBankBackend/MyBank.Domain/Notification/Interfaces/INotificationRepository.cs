using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyBank.Domain.Notification.Entities;

// INotificationRepository.cs

using NotificationEntity = MyBank.Domain.Notification.Entities.UserNotification;

namespace MyBank.Domain.Notification.Interfaces
{
    public interface INotificationRepository
    {
        Task<NotificationEntity> GetByIdAsync(string id);
        Task<IEnumerable<NotificationEntity>> GetByUserIdAsync(string userId);
        Task AddAsync(NotificationEntity notification);
        Task UpdateAsync(NotificationEntity notification);
    }
}