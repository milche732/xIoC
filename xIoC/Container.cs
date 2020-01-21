using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xIoC
{

    public class Container : IContainer
    {

        public IContainer Root { get; }
        public IScope Scope { get; }
        public IContractsRegistry _contractRegistry { get; }

        public Container(IContractsRegistry registry)
        {
            Root = this;
            _contractRegistry = registry ?? throw new ArgumentNullException(nameof(registry));
            Scope = new Scope(registry);
        }

        public Container(IContainer root, IContractsRegistry registry)
        {
            Root = root ?? throw new ArgumentNullException(nameof(root));
            _contractRegistry = registry ?? throw new ArgumentNullException(nameof(registry));
            Scope = new Scope(registry);
        }

        public IScope NewScope()
        {
            Scope scope = new Scope(this.Scope, _contractRegistry);
            return scope;
        }

        public object Resolve(Type type)
        {
            return new Resolver(Scope, _contractRegistry).Resolve(type);
        }
        public void Dispose()
        {
            if (Scope != null)
                Scope.Dispose();
        }
    }
}
