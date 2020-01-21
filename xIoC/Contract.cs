using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xIoC
{
    public class Contract
    {
        public Type ContractType { get; }

        public LifecycleType Lifecycle { get; }
        public IContractActivator Executer { get; }

        public Contract(Type contractType, IContractActivator executer) :
            this(contractType, executer, LifecycleType.Transient)
        {
        }
        public Contract(Type contractType, IContractActivator executer, LifecycleType lifecycle)
        {
            ContractType = contractType;
            Lifecycle = lifecycle;
            Executer = executer;
        }

        public override int GetHashCode()
        {
            return ContractType.GetHashCode();
        }
    }
}
