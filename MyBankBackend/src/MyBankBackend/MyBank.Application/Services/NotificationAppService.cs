using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using MyBank.Domain.Entities;
using MyBank.Domain.Interfaces;
using MyBank.Application.DTOs.Notifications;
using MyBank.Domain.Enums;
using MyBank.Application.DTOs.Account;
using MyBank.Domain.Exceptions;

namespace MyBank.Application.Services
{
    public class NotificationAppService : INotificationAppService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly INotificationSender _notificationSender;
        private readonly IMapper _mapper;

        public NotificationAppService(
            INotificationRepository notificationRepository,
            INotificationSender notificationSender,
            IMapper mapper)
        {
            _notificationRepository = notificationRepository;
            _notificationSender = notificationSender;
            _mapper = mapper;
        }

        public async Task<NotificationResponse> SendNotificationAsync(NotificationRequest request)
        {
            var notification = new Notification(
                request.Title,
                request.Message,
                Enum.Parse<NotificationType>(request.NotificationType),
                request.UserId);

            await _notificationRepository.AddAsync(notification);
            await _notificationSender.SendAsync(notification);

            return _mapper.Map<NotificationResponse>(notification);
        }

        public async Task<IEnumerable<NotificationResponse>> GetUserNotificationsAsync(Guid userId)
        {
            var notifications = await _notificationRepository.GetByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<NotificationResponse>>(notifications);
        }

        public async Task MarkAsReadAsync(Guid notificationId)
        {
            var notification = await _notificationRepository.GetByIdAsync(notificationId)
                ?? throw new DomainException("Notificação não encontrada");

            notification.MarkAsRead();
            await _notificationRepository.UpdateAsync(notification);
        }
    }
}