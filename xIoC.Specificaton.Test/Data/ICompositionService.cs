using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace xIoC.Specificaton.Test.Data
{
    public interface ICompositionService : IDisposable
    {
        void IncrementCount();
        int GetCount();
    }
}