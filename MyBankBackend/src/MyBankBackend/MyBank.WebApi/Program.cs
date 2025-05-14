using Microsoft.AspNetCore.Identity;
using MyBank.Application.Interfaces;
using MyBank.Application.Services;
using MyBank.Domain.Interfaces;
using MyBank.Infrastructure.Data;
using MyBank.Infrastructure.Persistence.Repositories;
using MyBank.Infrastructure.Services;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Adicione estas linhas na se��o de configura��o de servi�os (antes do builder.Build())

// Presentation/Program.cs

// Configura��o do DbContext
builder.Services.AddDbContext<MyBankDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registro dos reposit�rios
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

// Configura��o do JWT
var jwtConfig = builder.Configuration.GetSection("JwtConfig");
builder.Services.AddScoped<ITokenService>(_ =>
    new JwtTokenService(
        jwtConfig["Secret"],
        jwtConfig["Issuer"],
        int.Parse(jwtConfig["ExpiryInMinutes"])));

// Configura��o do AutoMapper
builder.Services.AddAutoMapper(typeof(Application.Mappings.DomainToDtoProfile));
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
