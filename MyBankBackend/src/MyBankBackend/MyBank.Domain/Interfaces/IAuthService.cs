
using System.Transactions;

using MyBank.Domain.Entities;
using MyBank.Domain.Models;

namespace MyBank.Domain.Interfaces
{
    public interface IAuthService
    {
        Task<AuthenticationResult> AuthenticateAsync(string cpf, string password);
        Task<TokenResponse> GenerateTokenAsync(User user);
        Task<bool> ValidateCredentialsAsync(string cpf, string secret);
    }
}

// Models para responses (coloque em Domain/Models/)
namespace MyBank.Domain.Models
{
    public record AuthenticationResult(bool Success, User? User, string ErrorMessage = "");
    public record TokenResponse(string Token, DateTime ExpiresAt);
}