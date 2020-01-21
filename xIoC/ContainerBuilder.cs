using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace xIoC
{
    public class ContainerBuilder
    {
        private readonly ContractsRegistry _contracts = new ContractsRegistry();

        public void Register<TContract>(Func<IResolve, TContract> func, LifecycleType lifecycle = LifecycleType.Transient)
           where TContract : class
        {
            _contracts.AddContract(
                new Contract(typeof(TContract), 
                new CreateByFactory(func),
                lifecycle));
        }

        public void Register<TContract, TImplementation>(LifecycleType lifecycle = LifecycleType.Transient)  
            where TContract : class
            where TImplementation : class
        {
            _contracts.AddContract(
                new Contract(typeof(TContract), 
                new CreateByType(typeof(TImplementation)),  lifecycle));
        }

        public void Register<TImplementation>(LifecycleType lifecycle = LifecycleType.Transient)
            where TImplementation : class
        {
            _contracts.AddContract(
                new Contract(typeof(TImplementation),
                new CreateByType(typeof(TImplementation)), lifecycle));
        }
        public void Register(Type selfType, LifecycleType lifecycle = LifecycleType.Transient)
        {
            _contracts.AddContract(
                new Contract(selfType,
                new CreateByType(selfType), lifecycle));
        }
       
        public IContainer Build()
        {
            return new Container(this._contracts);
        }
    }
}
