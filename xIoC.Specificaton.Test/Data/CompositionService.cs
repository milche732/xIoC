using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace xIoC.Specificaton.Test.Data
{
    public class CompositionService: ICompositionService
    {
        public CompositionService(IFirstService firstService, ISecondService secondService)
        {

        }

        private int _count = 0;
        public void IncrementCount()
        {
            _count++;
        }

        public int GetCount()
        {
            return _count;
        }

        public void Dispose()
        {
            Console.WriteLine($"disposing {this.GetType()}");
        }
    }
}