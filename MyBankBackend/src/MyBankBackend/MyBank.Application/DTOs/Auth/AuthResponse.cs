using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBank.Application.DTOs.Auth
{
    public record AuthResponse(
        string Token,
        DateTime ExpiresAt,
        string RefreshToken,
        Guid UserId,
        string UserName);
}
