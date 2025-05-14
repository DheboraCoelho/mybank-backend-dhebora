using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MyBank.Application.DTOs.Auth;
using System.Threading.Tasks;

namespace MyBank.Application.Interfaces
{
    public interface IAuthAppService
    {
        Task<AuthResponse> LoginAsync(LoginRequest request);
        Task<AuthResponse> RefreshTokenAsync(RefreshTokenRequest request);
        Task RequestPasswordResetAsync(string email);
        Task ResetPasswordAsync(ResetPasswordRequest request);
    }
}
