using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Application/DTOs/Notifications/NotificationRequest.cs
namespace MyBank.Application.DTOs.Notifications
{
    public record NotificationRequest(
        string Title,
        string Message,
        string NotificationType);
}

