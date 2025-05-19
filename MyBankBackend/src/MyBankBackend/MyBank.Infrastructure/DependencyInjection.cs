using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

// MyBank.Infrastructure/DependencyInjection.cs
using Microsoft.Extensions.DependencyInjection;
using MyBank.Application.Interfaces;
using MyBank.Application.Services;
using MyBank.Domain.Account.Interfaces;
using MyBank.Domain.Auth.Interfaces;
using MyBank.Domain.Notification.Interfaces;
using MyBank.Domain.Pix.Interfaces;
using MyBank.Infrastructure.Data;
using MyBank.Infrastructure.Data.Repositories;


namespace MyBank.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Registrar outros serviços de infraestrutura aqui
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPixRepository, PixRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();

            // Registrar serviços
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IPixService, PixService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IAuthService, AuthService>();
            return services;
        }
    }
}
