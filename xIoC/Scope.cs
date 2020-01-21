using System;
using System.Collections.Generic;
using System.Text;

namespace xIoC
{
    public class Scope : IScope
    {
        private readonly Queue<IDisposable> _disposableObjects = new Queue<IDisposable>();
        private readonly Dictionary<Type, object> _lookup = new Dictionary<Type, object>();

        public Scope(IContractsRegistry contractsRegistry)
        {
            ContractsRegistry = contractsRegistry ?? throw new ArgumentNullException(nameof(contractsRegistry));
            Root = this;
        }
        public Scope(IScope root, IContractsRegistry contractsRegistry)
        {
            Root = root ?? throw new ArgumentNullException(nameof(root));
            ContractsRegistry = contractsRegistry ?? throw new ArgumentNullException(nameof(contractsRegistry));
        }

        public IScope Root { get; }
        public IContractsRegistry ContractsRegistry { get; }

        public bool TryGetInstance(Contract contract, out object obj)
        {
            return _lookup.TryGetValue(contract.ContractType, out obj);
        }

        public void PutInstance(Contract contract, object obj)
        {
            _lookup.Add(contract.ContractType, obj);
            if(obj is IDisposable)
                TrackDisposal((IDisposable)obj);
        }

        public object Resolve(Type type)
        {
            return new Resolver(this, ContractsRegistry).Resolve(type);
        }

        public void TrackDisposal(IDisposable obj)
        {
            _disposableObjects.Enqueue(obj);
        }

        public void Dispose()
        {
            while (_disposableObjects.Count > 0)
            {
                _disposableObjects.Dequeue().Dispose();
            }
        }
    }
}
