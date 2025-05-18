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

        // DbSets para todas as entidades
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração da entidade User
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);

                entity.Property(u => u.Username)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(u => u.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(u => u.PasswordHash)
                    .IsRequired();

              
                // Índices únicos
                entity.HasIndex(u => u.Username)
                    .IsUnique();

                entity.HasIndex(u => u.Email)
                    .IsUnique();
            });

            // Configuração da entidade Account
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(a => a.Id);

                entity.Property(a => a.AccountNumber)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(a => a.Balance)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                entity.Property(a => a.CustomerId)
                    .IsRequired();

                entity.Property(a => a.CreatedAt)
                    .IsRequired();

                // Relacionamento com User (Customer)
                entity.HasOne<User>()
                    .WithMany(u => u.Accounts)
                    .HasForeignKey(a => a.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Relacionamento com Transaction
                

                // Índice único para número da conta
                entity.HasIndex(a => a.AccountNumber)
                    .IsUnique();
            });

            // Configuração da entidade Transaction
            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasKey(t => t.Id);

                entity.Property(t => t.Amount)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                entity.Property(t => t.Type)
                    .IsRequired()
                    .HasConversion<string>();

                entity.Property(t => t.CreatedAt)
                    .IsRequired();

                entity.Property(t => t.AccountId)
                    .IsRequired();


                // Índice para melhor performance em buscas por conta
                entity.HasIndex(t => t.AccountId);
            });
        }
    }
}