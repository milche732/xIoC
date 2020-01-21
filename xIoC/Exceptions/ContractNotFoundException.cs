using System;
using System.Collections.Generic;
using System.Text;

namespace xIoC.Exceptions
{
    public class ContractNotFoundException:Exception
    {
        public Type ContractType { get; }
        public override string Message => $"Cannot resolve type {ContractType}.";
        public ContractNotFoundException(Type contractType)
        {
            ContractType = contractType;
        }
    }
}
