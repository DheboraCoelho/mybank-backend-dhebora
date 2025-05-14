using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Application/DTOs/Notifications/CreateNotificationRequest.cs
namespace MyBank.Application.DTOs.Notifications
{
    public record CreateNotificationRequest(
        Guid UserId,
        string Title,
        string Message,
        string NotificationType);
}
