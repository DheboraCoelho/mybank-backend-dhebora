using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Application/DTOs/Auth/TokenResponse.cs
namespace MyBank.Application.DTOs.Auth
{
    public record TokenResponse(
        string Token,
        DateTime ExpiresAt,
        string RefreshToken);
}
