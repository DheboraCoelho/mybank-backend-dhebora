using Microsoft.AspNetCore.Identity;
using MyBank.Application.Interfaces;
using MyBank.Application.Services;
using MyBank.Domain.Interfaces;
using MyBank.Infrastructure.Data;
using MyBank.Infrastructure.Persistence.Repositories;
using MyBank.Infrastructure.Services;
using MyBank.Domain.Entities;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.Extensions.Configuration;
using MyBank.Application.DTOs;

var builder = WebApplication.CreateBuilder(args);
services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
// Configuração JWT
var jwtSettings = Configuration.GetSection("JwtSettings");
services.Configure<JwtSettings>(jwtSettings);

services.AddSingleton<ITokenService>(provider =>
    new JwtTokenService(
        jwtSettings["SecretKey"],
        int.Parse(jwtSettings["ExpirationHours"]),
        jwtSettings["Issuer"],
        jwtSettings["Audience"]
    ));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Configure layers
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
// Configuração do DbContext
services.AddDbContext<MyBankDbContext>(options =>
    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

// Registro do repositório
services.AddScoped<IUserRepository, UserRepository>();

// Configuração JWT
var jwtSettings = Configuration.GetSection("JwtSettings");
services.Configure<JwtSettings>(jwtSettings);

services.AddSingleton<ITokenService>(provider =>
    new TokenService(
        jwtSettings["SecretKey"],
        int.Parse(jwtSettings["ExpirationHours"]),
        jwtSettings["Issuer"],
        jwtSettings["Audience"]
    ));

// Registro dos serviços
services.AddScoped<IAuthAppService, AuthAppService>();
services.AddScoped<ITokenService, TokenService>();
services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Adicione estas linhas na seção de configuração de serviços (antes do builder.Build())



app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
