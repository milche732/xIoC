using System;
using System.Collections.Generic;
using System.Text;

namespace xIoC.Specificaton.Test.Data
{
    public class DisposableComponent : IDisposable
    {
        private bool disposed = false;
        public bool IsDisposed => disposed;
        public void Dispose()
        {
            disposed = true;
        }
    }
}
