using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBank.Application.DTOs
{
    public class CreateAccountRequest
    {
        public string AccountNumber { get; set; }  
        public string Agency { get; set; }        //
        public string OwnerId { get; set; }
    }

    public class AccountResponse
    {
        public string Id { get; set; }
        public string AccountNumber { get; set; }
        public string Agency { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
        public string Currency { get; set; }
        public string OwnerId { get; set; }

    }

    public class TransactionRequest
    {
        public decimal Amount { get; set; }  // Será convertido para Money
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser positivo")]
       
        [Required]
        public string Description { get; set; }
    }
}

