using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MyBank.Application.DTOs.Accounts
{
    public class TransactionRequest
    {
        public Guid AccountId { get; set; }  // Corrigido de "Accountld" para "AccountId"
        public decimal Amount { get; set; }
        public string Description { get; set; }
    }
}
