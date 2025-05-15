using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBank.Application.DTOs
{
    public class JwtSettings
    {
        public string SecretKey { get; set; }
        public int ExpirationHours { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}