using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Infrastructure/Persistence/Repositories/AccountRepository.cs
using Microsoft.EntityFrameworkCore;
using MyBank.Domain.Entities;
using MyBank.Domain.Interfaces;
using MyBank.Infrastructure.Data;

namespace MyBank.Infrastructure.Persistence.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly MyBankDbContext _context;

        public AccountRepository(MyBankDbContext context)
        {
            _context = context;
        }

        public async Task<Account> GetByIdAsync(Guid id)
        {
            return await _context.Accounts
                .Include(a => a.Transactions)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task AddAsync(Account account)
        {
            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();
        }

        // Implementar outros métodos da interface...
    }
}
