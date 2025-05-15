using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Infrastructure/Persistence/Repositories/CustomerRepository.cs
using Microsoft.EntityFrameworkCore;
using MyBank.Domain.Entities;
using MyBank.Domain.Interfaces;
using MyBank.Infrastructure.Data;
using System;
using System.Threading.Tasks;


namespace MyBank.Infrastructure.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly MyBankDbContext _context;

        public CustomerRepository(MyBankDbContext context)
        {
            _context = context;
        }

        public async Task<Customer> GetByIdAsync(Guid id)
        {
            return await _context.Customers.FindAsync(id);
        }

        public async Task<Customer> GetByCpfAsync(string cpf)
        {
            return await _context.Customers
                .FirstOrDefaultAsync(c => c.Cpf == cpf);
        }

        public async Task<Customer> GetByEmailAsync(string email)
        {
            return await _context.Customers
                .FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task AddAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Customer customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Guid customerId)
        {
            return await _context.Customers
                .AnyAsync(c => c.Id == customerId);
        }
    }
}