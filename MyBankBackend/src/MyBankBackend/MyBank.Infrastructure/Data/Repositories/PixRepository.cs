using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// MyBankAPI.Infrastructure/Data/Repositories/PixRepository.cs
using Microsoft.EntityFrameworkCore;
using MyBank.Domain.Pix.Entities;
using MyBank.Domain.Pix.Interfaces;
using MyBank.Infrastructure.Data;
using System.Threading.Tasks;



namespace MyBank.Infrastructure.Data.Repositories
{
    public class PixRepository : IPixRepository
    {
        private readonly AppDbContext _context;

        public PixRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PixTransaction> GetByIdAsync(string id)
        {
            return await _context.PixTransactions.FindAsync(id);
        }

        public async Task AddAsync(PixTransaction transaction)
        {
            await _context.PixTransactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(PixTransaction transaction)
        {
            _context.PixTransactions.Update(transaction);
            await _context.SaveChangesAsync();
        }
    }
}