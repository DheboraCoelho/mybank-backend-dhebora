using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// MyBankAPI.Application/Services/NotificationService.cs
using MyBank.Domain.Notification.Entities;

using MyBank.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using MyBank.Application.Interfaces;
using MyBank.Domain.Notification.Interfaces;


namespace MyBank.Application.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<NotificationResponse> CreateNotification(CreateNotificationRequest request)
        {
            var notification = new UserNotification(request.UserId, request.Title, request.Message);
            await _notificationRepository.AddAsync(notification);

            return new NotificationResponse
            {
                Id = notification.Id,
                Title = notification.Title,
                Message = notification.Message,
                CreatedAt = notification.CreatedAt,
                IsRead = notification.IsRead
            };
        }

        public async Task<IEnumerable<NotificationResponse>> GetUserNotifications(string userId)
        {
            var notifications = await _notificationRepository.GetByUserIdAsync(userId);
            return notifications.Select(n => new NotificationResponse
            {
                Id = n.Id,
                Title = n.Title,
                Message = n.Message,
                CreatedAt = n.CreatedAt,
                IsRead = n.IsRead
            });
        }

        public async Task MarkAsRead(string notificationId)
        {
            var notification = await _notificationRepository.GetByIdAsync(notificationId);
            if (notification == null) return;

            notification.MarkAsRead();
            await _notificationRepository.UpdateAsync(notification);
        }
    }
}