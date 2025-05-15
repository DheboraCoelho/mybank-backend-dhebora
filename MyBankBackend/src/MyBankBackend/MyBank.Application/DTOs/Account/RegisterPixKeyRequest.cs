using MyBank.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBank.Application.DTOs.Accounts
{
    public class RegisterPixKeyRequest
    {
        public Guid AccountId { get; set; }
        public string Key { get; set; }
        public PixKeyType Type { get; set; }
    }
}