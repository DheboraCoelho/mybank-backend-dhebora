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




namespace MyBank.Infrastructure.Data
{
    public class MyBankDbContext : DbContext
    {
        public MyBankDbContext(DbContextOptions<MyBankDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuração da entidade User
            modelBuilder.Entity<User>(entity =>
            {
                // Chave primária
                entity.HasKey(u => u.Id);

                // Índices únicos
                entity.HasIndex(u => u.Username).IsUnique();
                entity.HasIndex(u => u.Email).IsUnique();
                entity.HasIndex(u => u.RefreshToken).IsUnique();
                entity.HasIndex(u => u.PasswordResetToken).IsUnique();

                // Configurações de campos
                entity.Property(u => u.Username).IsRequired().HasMaxLength(50);
                entity.Property(u => u.Email).IsRequired().HasMaxLength(100);
                entity.Property(u => u.PasswordHash).IsRequired();
                entity.Property(u => u.RefreshToken).HasMaxLength(100);
                entity.Property(u => u.PasswordResetToken).HasMaxLength(100);
            });
        }
    }
}