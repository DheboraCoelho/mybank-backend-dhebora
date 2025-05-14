using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Application/DTOs/Notifications/NotificationResponse.cs
namespace MyBank.Application.DTOs.Notifications
{
    public record NotificationResponse(
        Guid Id,
        string Title,
        string Message,
        string NotificationType,
        DateTime CreatedAt,
        bool IsRead);
}
