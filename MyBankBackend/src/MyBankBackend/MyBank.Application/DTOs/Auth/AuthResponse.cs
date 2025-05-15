using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

namespace MyBank.Application.DTOs.Auth
{
    public class AuthResponse
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public string RefreshToken { get; set; }
        public Guid UserId { get; set; }
        public string Username { get; set; }
    }
}