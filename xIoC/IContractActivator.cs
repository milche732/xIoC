using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xIoC
{
    public interface IContractActivator
    {
       object CreateInstance(IResolve context);
    }
}
