using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Threading.Tasks;

namespace MyBank.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> Authenticate(string email, string password);
        Task<bool> Register(string name, string email, string cpf, string password);
    }
}