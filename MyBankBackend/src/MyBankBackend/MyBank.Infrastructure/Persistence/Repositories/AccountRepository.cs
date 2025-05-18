using Microsoft.EntityFrameworkCore;
using MyBank.Domain.Entities;
using MyBank.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBank.Infrastructure.Data.Repositories
{
    public class AccountRepository 
    {
        private readonly MyBankDbContext _context;

        public AccountRepository(MyBankDbContext context)
        {
            _context = context;
        }

        public async Task<Account> GetByIdAsync(Guid id)
        {
            return await _context.Accounts.FindAsync(id);
        }

        // Implementar outros métodos do IAccountRepository
    }
}