using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyBank.Application.Interfaces;
using MyBank.Application.Mappings;
using MyBank.Application.Services;
using MyBank.Infrastructure.Data;
using MyBank.Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBank.Infrastructure.Presentation.Config
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MyBankDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(DomainToDtoProfile));

            services.AddScoped<IAccountAppService, AccountAppService>();

            return services;
        }
    }
}
