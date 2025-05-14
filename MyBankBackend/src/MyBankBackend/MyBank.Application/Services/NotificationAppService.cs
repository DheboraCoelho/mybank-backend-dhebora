using System.Threading.Tasks;
using MyBank.Application.DTOs.Notifications;
using MyBank.Application.Interfaces;
using MyBank.Domain.Interfaces;

namespace MyBank.Application.Services
{
    public class NotificationAppService : INotificationAppService
    {
        private readonly INotificationSender _notificationSender;

        public NotificationAppService(INotificationSender notificationSender)
        {
            _notificationSender = notificationSender;
        }

        public async Task SendNotificationAsync(NotificationRequest request)
        {
            await _notificationSender.SendAsync(request);
        }
    }
}
