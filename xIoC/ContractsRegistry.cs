using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xIoC
{
    public interface IContractsRegistry
    {
         bool TryGetContract(Type type, out Contract contract);
         void AddContract(Contract contract);
    }
    public class ContractsRegistry:IContractsRegistry
    {
        private readonly Dictionary<Type, Contract> _typeContractDict = new Dictionary<Type, Contract>();

        public bool TryGetContract(Type type, out Contract contract)
        {
            return _typeContractDict.TryGetValue(type, out contract);
        }

        public void AddContract(Contract contract)
        {
            if (_typeContractDict.ContainsKey(contract.ContractType))
            {
                _typeContractDict[contract.ContractType] = contract;
            }
            else
            {
                _typeContractDict.Add(contract.ContractType, contract);
            }
        }
    }
}
