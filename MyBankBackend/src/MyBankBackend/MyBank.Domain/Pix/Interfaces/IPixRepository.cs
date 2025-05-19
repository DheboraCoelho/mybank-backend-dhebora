using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



using System.Threading.Tasks;
using MyBank.Domain.Pix.Entities;

namespace MyBank.Domain.Pix.Interfaces
{
    public interface IPixRepository
    {
        Task<PixTransaction> GetByIdAsync(string id);
        Task AddAsync(PixTransaction transaction);
        Task UpdateAsync(PixTransaction transaction);
    }
}