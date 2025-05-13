using MyBank.Domain.Entities;
using MyBank.Domain.Enums;
using MyBank.Domain.Models;
using MyBank.Domain.ValueObjects;

namespace MyBank.Domain.Interfaces
{
    public interface IPixService
    {
        Task<IEnumerable<PixKey>> GetKeysByAccountAsync(Guid accountId);
        Task<PixEvaluationResult> EvaluateKeyAsync(string key, PixKeyType type);
        Task<PixTransferResult> TransferAsync(Account source, Account destination, Amount amount);
        Task<bool> ConfirmTransferAsync(Guid transactionId);
    }
}

// Models para responses (coloque em Domain/Models/)
namespace MyBank.Domain.Models
{
    public record PixEvaluationResult(bool IsValid, ExternalAccount? Account = null);
    public record PixTransferResult(bool Success, Guid TransactionId, string? ErrorMessage = null);
}