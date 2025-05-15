
using MyBank.Domain.Entities;
using MyBank.Domain.Interfaces;
using MyBank.Infrastructure.Data;
using System;
using System.Threading.Tasks;

namespace MyBank.Infrastructure.Data.Repositories
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
            return await _context.Accounts.FindAsync(id);
        }

        public async Task<Account> GetByIdWithTransactionsAsync(Guid id)
        {
            return await _context.Accounts
                .Include(a => a.Transactions)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task UpdateAsync(Account account)
        {
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Guid accountId)
        {
            return await _context.Accounts
                .AnyAsync(a => a.Id == accountId);
        }
    }
}