using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Threading.Tasks;

namespace MyBank.Application.Interfaces
{
    public interface INotificationSender
    {
        Task SendNotificationAsync(string userId, string title, string message);
    }
}
