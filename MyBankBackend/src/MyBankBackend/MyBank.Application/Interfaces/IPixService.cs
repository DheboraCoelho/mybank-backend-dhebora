using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// MyBankAPI.Application/Interfaces/IPixService.cs
using MyBank.Application.DTOs;
using System.Threading.Tasks;

namespace MyBank.Application.Interfaces
{
    public interface IPixService
    {
        Task<PixResponse> CreatePixTransaction(PixRequest request);
        Task<PixResponse> GetPixTransaction(string id);
    }
}
