using AutoMapper;
using MyBank.Domain.Entities;
using MyBank.Domain.Interfaces;
using MyBank.Application.DTOs.Notifications;
using MyBank.Domain.Enums;
using MyBank.Application.Interfaces;

namespace MyBank.Application.Services
{
    public class NotificationAppService : INotificationAppService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly INotificationSender _emailSender;
        private readonly INotificationSender _smsSender;
        private readonly IMapper _mapper;

        public NotificationAppService(
            INotificationRepository notificationRepository,
            IEnumerable<INotificationSender> notificationSenders,
            IMapper mapper)
        {
            _notificationRepository = notificationRepository;
            _emailSender = notificationSenders.First(s => s.GetType().Name == "EmailNotificationSender");
            _smsSender = notificationSenders.First(s => s.GetType().Name == "SmsNotificationSender");
            _mapper = mapper;
        }

        public async Task<NotificationResponse> CreateAndSendAsync(CreateNotificationRequest request)
        {
            var notification = new Notification(
                request.Title,
                request.Message,
                Enum.Parse<NotificationType>(request.NotificationType),
                request.UserId);

            await _notificationRepository.AddAsync(notification);

            // Envia por todos os canais
            await _emailSender.SendAsync(notification);
            await _smsSender.SendAsync(notification);

            return _mapper.Map<NotificationResponse>(notification);
        }

        public async Task<IEnumerable<NotificationResponse>> GetByUserAsync(Guid userId)
        {
            var notifications = await _notificationRepository.GetByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<NotificationResponse>>(notifications);
        }
    }
}