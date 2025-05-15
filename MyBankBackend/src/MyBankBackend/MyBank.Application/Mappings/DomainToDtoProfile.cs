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
                CreateMap<Account, AccountResponse>();
                CreateMap<Transaction, TransactionResponse>()
                    .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()));
                CreateMap<PixKey, PixKeyResponse>()
                    .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()));
            }
        }
    }
