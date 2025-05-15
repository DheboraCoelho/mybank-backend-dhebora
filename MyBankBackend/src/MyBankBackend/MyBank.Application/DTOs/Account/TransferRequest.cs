using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Application/DTOs/Accounts/TransferRequest.cs
namespace MyBank.Application.DTOs.Accounts
{
    public class TransferRequest
    {
        public Guid FromAccountId { get; set; }  // Corrigido de "FromAccountld"
        public Guid ToAccountId { get; set; }    // Corrigido de "ToAccountld"
        public decimal Amount { get; set; }
        public string Description { get; set; }
    }
}