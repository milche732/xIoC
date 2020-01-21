using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xIoC
{
    public interface IContainer :IResolve, IDisposable
    {
        IScope NewScope();
    }
}
