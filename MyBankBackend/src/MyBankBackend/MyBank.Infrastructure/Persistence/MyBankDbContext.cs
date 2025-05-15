using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

// Infrastructure/Data/MyBankDbContext.cs
using MyBank.Domain.Entities;
using MyBank.Domain.Interfaces;
using MyBank.Domain.Enums;
using MyBank.Domain.Exceptions;
using MyBank.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;  // Adicione esta linha

using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyBank.Domain.Entities;
using System.Threading.Tasks;

namespace MyBank.Infrastructure.Data
{
    public class MyBankDbContext : DbContext
    {
        public MyBankDbContext(DbContextOptions<MyBankDbContext> options)
            : base(options) { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        // Adicione este método explicitamente
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(a => a.Id);
                entity.HasMany(a => a.Transactions)
                      .WithOne()
                      .HasForeignKey(t => t.AccountId);
            });
        }
    }
}