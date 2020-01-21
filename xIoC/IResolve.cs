using System;
using System.Collections.Generic;
using System.Text;

namespace xIoC
{
    public interface IResolve
    {
        object Resolve(Type type);
    }
}
