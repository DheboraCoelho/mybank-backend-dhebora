using Microsoft.EntityFrameworkCore;
using MyBank.Domain.Account.Entities;
using MyBank.Domain.Auth.Entities;
using MyBank.Domain.Notification.Entities;
using MyBank.Domain.Pix.Entities;
using MyBank.Domain.Account.ValueObjects.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyBank.Domain.ValueObjects.Shared;

namespace MyBank.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<PixTransaction> PixTransactions { get; set; }
        public DbSet<UserNotification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurações do modelo
            base.OnModelCreating(modelBuilder);

            // Configuração do Document como Value Object
            modelBuilder.Entity<User>()
                .Property(u => u.Document)
                .HasConversion(
                    v => v.Value,          
                    v => Document.Create(v) 
                );
        }


    }
}

