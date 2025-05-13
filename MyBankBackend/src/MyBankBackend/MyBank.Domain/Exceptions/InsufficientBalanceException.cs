using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBank.Domain.Exceptions
{
    public class InsufficientBalanceException : DomainException
    {
        public InsufficientBalanceException()
            : base("Saldo insuficiente para realizar a operação") { }
    }
}
