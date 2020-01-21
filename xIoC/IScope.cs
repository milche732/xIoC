using System;
using System.Collections.Generic;
using System.Text;

namespace xIoC
{
    public interface IScope : IResolve, IDisposable
    {
        IScope Root { get; }

        void PutInstance(Contract contract, object obj);
        bool TryGetInstance(Contract contract, out object obj);
        void TrackDisposal(IDisposable obj);
    }
}
