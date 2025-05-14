using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Application/DTOs/Notification/NotificationResponse.cs
namespace MyBank.Application.DTOs.Notifications
{
    // Application/DTOs/Notifications/NotificationResponse.cs
    public record NotificationResponse(
        Guid Id,
        string Title,
        string Message,
        string NotificationType, // <-- Mapeado do Type da entidade
        DateTime CreatedAt,
        bool IsRead);
}