using System;
using System.Collections.Generic;
using System.Text;

namespace xIoC.Exceptions
{
    public class CycleReferenceException: Exception
    {
        public Contract Contract { get; } 
        public CycleReferenceException(Contract contract)
        {
            Contract = contract ?? throw new ArgumentNullException(nameof(contract));
        }
        public override string Message => $"Cycle resolving of type {Contract.ContractType}";
    }
}
