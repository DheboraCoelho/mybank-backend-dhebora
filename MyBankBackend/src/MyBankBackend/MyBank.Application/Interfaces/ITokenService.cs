using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MyBank.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace MyBank.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
        string GenerateRefreshToken();
        string GeneratePasswordResetToken();
    }
}
