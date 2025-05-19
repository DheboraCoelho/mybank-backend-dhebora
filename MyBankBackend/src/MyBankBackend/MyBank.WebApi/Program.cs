using Microsoft.OpenApi.Models;
using MyBank.Infrastructure;
using MyBank.Infrastructure.Data;
using MyBank.Application.Services;
using MyBank.Application.Interfaces;
using MyBank.Domain.Notification.Interfaces;
using MyBank.Domain.Account.Interfaces;
using MyBank.Domain.Auth.Interfaces;
using MyBank.Domain.Pix.Interfaces;
using MyBank.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configure apenas o necessário para sua API bancária
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "MyBank API",
        Version = "v1",
        Description = "API para operações bancárias",
        Contact = new OpenApiContact { Name = "Suporte", Email = "suporte@mybank.com" }
    });
});
// Configure database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddInfrastructure(connectionString);

// Register application services
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IPixService, PixService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IAuthService, AuthService>();
// Registrar repositórios
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPixRepository, PixRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
var app = builder.Build();

// Configuração do pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyBank API v1");
        c.DisplayRequestDuration(); // Mostra tempo de resposta
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
//Apply migrations
// Apply migrations
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating the database.");
    }
}
app.Run();