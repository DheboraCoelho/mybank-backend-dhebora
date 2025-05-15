using AutoMapper;
using MyBank.Application.DTOs.Notifications;
using MyBank.Application.Interfaces;
using MyBank.Domain.Entities;
using MyBank.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MyBank.Application.Services
{
    public class NotificationAppService : INotificationAppService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IEnumerable<INotificationSender> _notificationSenders;
        private readonly IMapper _mapper;

        public NotificationAppService(
            INotificationRepository notificationRepository,
            IEnumerable<INotificationSender> notificationSenders,
            IMapper mapper)
        {
            _notificationRepository = notificationRepository;
            _notificationSenders = notificationSenders;
            _mapper = mapper;
        }

        public async Task<NotificationResponse> SendNotificationAsync(CreateNotificationRequest request)
        {
            var notification = new Notification(
                request.UserId,
                request.Title,
                request.Message,
                request.NotificationType,
                DateTime.UtcNow);

            await _notificationRepository.AddAsync(notification);

            // Envia notificação por todos os canais configurados
            foreach (var sender in _notificationSenders)
            {
                await sender.SendNotificationAsync(
                    notification.UserId.ToString(),
                    notification.Title,
                    notification.Message);
            }

            return _mapper.Map<NotificationResponse>(notification);
        }

        public async Task<IEnumerable<NotificationResponse>> GetUserNotificationsAsync(Guid userId)
        {
            var notifications = await _notificationRepository.GetByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<NotificationResponse>>(notifications);
        }

        public async Task MarkNotificationAsReadAsync(Guid notificationId)
        {
            var notification = await _notificationRepository.GetByIdAsync(notificationId);
            if (notification != null)
            {
                notification.MarkAsRead();
                await _notificationRepository.UpdateAsync(notification);
            }
        }

        public async Task<int> GetUnreadCountAsync(Guid userId)
        {
            return await _notificationRepository.GetUnreadCountAsync(userId);
        }
    }
}