using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MyBank.Domain.Entities;
using MyBank.Domain.Entities;
using System.Threading.Tasks;

namespace MyBank.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer> GetByIdAsync(Guid id);
        Task<Customer> GetByCpfAsync(string cpf);
        Task<Customer> GetByEmailAsync(string email);
        Task AddAsync(Customer customer);
        Task UpdateAsync(Customer customer);
        Task<bool> ExistsAsync(Guid customerId);
    }
}