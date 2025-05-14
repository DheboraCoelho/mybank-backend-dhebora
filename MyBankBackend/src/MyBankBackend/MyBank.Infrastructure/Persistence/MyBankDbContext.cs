using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

// Infrastructure/Data/MyBankDbContext.cs
using Microsoft.EntityFrameworkCore;
using MyBank.Domain.Entities;

namespace MyBank.Infrastructure.Data
{
    public class MyBankDbContext : DbContext
    {
        public MyBankDbContext(DbContextOptions<MyBankDbContext> options)
            : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<BankTransaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurações do modelo
            modelBuilder.Entity<Account>()
                .HasMany(a => a.Transactions)
                .WithOne()
                .HasForeignKey(t => t.AccountId);
        }
    }
}
