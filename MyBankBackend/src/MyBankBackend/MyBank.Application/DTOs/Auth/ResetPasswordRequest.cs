using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Application/DTOs/Auth/ResetPasswordRequest.cs
namespace MyBank.Application.DTOs.Auth
{
    public record ResetPasswordRequest(
        string Email,
        string Token,
        string NewPassword);
}