using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xIoC
{
    public class CreateByFactory : IContractActivator
    {
        Func<IResolve, object> _func;
        public CreateByFactory(Func<IResolve,object> func) {
            _func = func;
        }
        public object CreateInstance(IResolve resolvingContext)
        {
            return _func(resolvingContext);
        }
    }
}
