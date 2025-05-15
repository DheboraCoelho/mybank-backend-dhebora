using MyBank.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBank.Domain.Interfaces
{
    public interface INotificationRepository
    {
        Task AddAsync(Notification notification);
        Task UpdateAsync(Notification notification);
        Task<Notification> GetByIdAsync(Guid id);
        Task<IEnumerable<Notification>> GetByUserIdAsync(Guid userId);
        Task<int> GetUnreadCountAsync(Guid userId);
    }
}