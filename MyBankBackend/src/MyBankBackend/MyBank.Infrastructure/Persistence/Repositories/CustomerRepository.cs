using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Infrastructure/Persistence/Repositories/CustomerRepository.cs
using Microsoft.EntityFrameworkCore;
using MyBank.Core.ValueObjects;
using MyBank.Domain.Entities;
using MyBank.Domain.Interfaces;
using MyBank.Domain.ValueObjects;
using MyBank.Infrastructure.Data;

namespace MyBank.Infrastructure.Persistence.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly MyBankDbContext _context;

        public CustomerRepository(MyBankDbContext context)
        {
            _context = context;
        }

        public async Task<Customer> GetByCpfAsync(Cpf cpf)
        {
            return await _context.Customers
                .FirstOrDefaultAsync(c => c.Cpf.Value == cpf.Value);
        }

        // Implementar outros métodos da interface...
    }
}
