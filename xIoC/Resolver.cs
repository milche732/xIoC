using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using xIoC.Exceptions;

namespace xIoC
{
    
    public class Resolver : IResolve
    {
        private ResolvingContext _resolvingContext;
        public Resolver(IScope scope, IContractsRegistry registry)
        {
            Scope = scope ?? throw new ArgumentNullException(nameof(scope));
            Registry = registry ?? throw new ArgumentNullException(nameof(registry));
            _resolvingContext = new ResolvingContext(this);
        }

        public IScope Scope { get; }
        public IContractsRegistry Registry { get; }

        public object Resolve(Type contractType)
        {
            object instance = null;
            
            Stack<Type> typeStack = new Stack<Type>();
            Contract contract;

            if(!Registry.TryGetContract(contractType, out contract))
            {
                throw new ContractNotFoundException(contractType);
            }
            
            _resolvingContext.StartResolving(contract);

            Func<IScope, object> create = (IScope x) =>
            {
                return contract.Executer.CreateInstance(this);
            };

            if (contract.Lifecycle == LifecycleType.Singleton)
            {
                CreateSingleton(contract, Scope.Root, create, out instance);
            }
            else if (contract.Lifecycle == LifecycleType.Transient)
            {
                instance = create(Scope);

                if (instance is IDisposable)
                {
                    if (_resolvingContext.HasSinglton())
                        Scope.Root.TrackDisposal((IDisposable)instance);
                    else
                        Scope.TrackDisposal((IDisposable)instance);
                }
            }

            _resolvingContext.EndResolving();

            return instance;
        }

        private bool CreateSingleton(Contract contract, IScope root, Func<IScope, object> action, out object result)
        {
            if (!root.TryGetInstance(contract, out result))
                lock (root)
                    if (!root.TryGetInstance(contract, out result))
                    {
                        result = action(root);
                        root.PutInstance(contract, result);
                        return true;
                    }
            return false;
        }
    }
}
