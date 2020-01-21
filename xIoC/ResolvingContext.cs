using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xIoC.Exceptions;

namespace xIoC
{
    public class ResolvingContext
    {
        public IResolve Resolver { get; }
        private readonly Stack<Contract> _resolvingContracts = new Stack<Contract>();
        
        public ResolvingContext(IResolve resolver)
        {
            Resolver = resolver;
        }
        public void StartResolving(Contract contract)
        {
            if (_resolvingContracts.Contains(contract))
                throw new CycleReferenceException(contract);
            _resolvingContracts.Push(contract);
        }

        public bool HasSinglton()
        {
            return _resolvingContracts.Any(x => x.Lifecycle == LifecycleType.Singleton);
        }
        
        public void EndResolving()
        {
            _resolvingContracts.Pop();
        }
    }
}
