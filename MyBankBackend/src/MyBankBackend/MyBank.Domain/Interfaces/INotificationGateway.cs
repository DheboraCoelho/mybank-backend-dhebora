using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MyBank.Domain.Entities;
using MyBank.Domain.ValueObjects;

namespace MyBank.Domain.Interfaces
{
    public interface INotificationGateway
    {
        Task SendTransferNotificationAsync(Account source, Account destination, Amount amount);
        Task SendAuthenticationNotificationAsync(User user, string deviceInfo);
    }
}
