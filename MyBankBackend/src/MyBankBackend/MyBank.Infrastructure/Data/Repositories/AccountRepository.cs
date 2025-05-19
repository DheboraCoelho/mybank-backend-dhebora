using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// MyBankAPI.Infrastructure/Data/Repositories/AccountRepository.cs
using Microsoft.EntityFrameworkCore;
using MyBank.Domain.Account.Entities;
using MyBank.Domain.Account.Interfaces;

using System.Threading.Tasks;
using MyBank.Domain.Account.Entities;
using MyBank.Domain.Account.Interfaces;
using MyBank.Infrastructure.Data;

namespace MyBank.Infrastructure.Data.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _context;

        public AccountRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<BankAccount> GetByIdAsync(string id)
        {
            return await _context.BankAccounts.FindAsync(id);
        }

        public async Task AddAsync(BankAccount account)
        {
            await _context.BankAccounts.AddAsync(account);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(BankAccount account)
        {
            _context.BankAccounts.Update(account);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var account = await GetByIdAsync(id);
            if (account != null)
            {
                _context.BankAccounts.Remove(account);
                await _context.SaveChangesAsync();
            }
        }
    }
}
