
using MyBank.Application.DTOs;
using MyBank.Application.Interfaces;
using MyBank.Application.Services;
using MyBank.Domain.Interfaces;
using MyBank.Infrastructure.Data;
using MyBank.Infrastructure.Persistence.Repositories;
using MyBank.Infrastructure.Services;
using System.Text;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        // ... outras configurações

        // 1. Configuração do JWT
        var jwtSettings = Configuration.GetSection("JwtSettings");
        services.Configure<JwtSettings>(jwtSettings);

        // 2. Configuração da Autenticação JWT
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["Issuer"],
                ValidAudience = jwtSettings["Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSettings["SecretKey"])),
                ClockSkew = TimeSpan.Zero
            };
        });

        // 3. Configuração dos Serviços de Autenticação
        services.AddScoped<ITokenService>(provider =>
            new TokenService(
                jwtSettings["SecretKey"],
                int.Parse(jwtSettings["ExpirationHours"])
            ));

        services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
        services.AddScoped<IEmailService, SmtpEmailService>(); // Ou sua implementação
        services.AddScoped<IAuthAppService, AuthAppService>();

        // 4. Configuração do CORS (se necessário)
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });
        });

        // ... outras configurações (MVC, Swagger, etc)
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // ... outras configurações

        app.UseRouting();

        // Habilitar CORS (antes do UseAuthentication)
        app.UseCors("AllowAll");

        // Habilitar autenticação e autorização
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        // ... outras configurações
        // Configuração do DbContext
        services.AddDbContext<MyBankDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

        // Registro do repositório
        services.AddScoped<IUserRepository, UserRepository>();

        // Registro dos serviços
        services.AddScoped<IAuthAppService, AuthAppService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
    }
}