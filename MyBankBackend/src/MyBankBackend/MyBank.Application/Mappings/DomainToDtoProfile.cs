using AutoMapper;
using MyBank.Domain.Entities;
using MyBank.Domain.Enums;
using MyBank.Domain.ValueObjects;
using MyBank.Application.DTOs.Accounts;
using MyBank.Application.DTOs.Auth;
using MyBank.Application.DTOs.Notifications;

namespace MyBank.Application.Mappings
{
    public class DomainToDtoProfile : Profile
    {
        public DomainToDtoProfile()
        {
            // Account Mappings
            CreateMap<Account, AccountResponse>()
                .ForMember(dest => dest.Currency,
                    opt => opt.MapFrom(src => src.Balance.Currency.ToString()))
                .ForMember(dest => dest.Balance,
                    opt => opt.MapFrom(src => src.Balance.Value));

            // Corrigido: Transaction → BankTransaction
            CreateMap<BankTransaction, TransactionResponse>()
                .ForMember(dest => dest.Type,
                    opt => opt.MapFrom(src => src.Type.ToString()))
                .ForMember(dest => dest.Date,
                    opt => opt.MapFrom(src => src.TransactionDate))
                .ForMember(dest => dest.Description,
                    opt => opt.MapFrom(src => GetTransactionDescription(src)));

            // Auth Mappings
            CreateMap<User, TokenResponse>()
                .ForMember(dest => dest.Token,
                    opt => opt.Ignore())
                .ForMember(dest => dest.RefreshToken,
                    opt => opt.Ignore());

            // Notification Mappings
            CreateMap<Notification, NotificationResponse>()
                .ForMember(dest => dest.NotificationType,
                    opt => opt.MapFrom(src => src.Type.ToString()));
        }

        private string GetTransactionDescription(BankTransaction transaction)
        {
            return transaction.Type switch
            {
                TransactionType.Deposit => "Depósito realizado",
                TransactionType.Withdraw => "Saque realizado",
                TransactionType.Transfer => "Transferência realizada",
                _ => "Transação bancária"
            };
        }
    }
}