using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Application/DTOs/ErrorResponse.cs
namespace MyBank.Application.DTOs
{
    public record ErrorResponse(
        string ErrorCode,
        string Message,
        Dictionary<string, string>? Details = null);
}