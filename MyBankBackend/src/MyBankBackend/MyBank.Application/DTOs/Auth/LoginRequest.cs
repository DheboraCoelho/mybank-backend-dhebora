using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Application/DTOs/Auth/LoginRequest.cs
namespace MyBank.Application.DTOs.Auth
{
    public record LoginRequest(
        string Cpf,
        string Password);
}
