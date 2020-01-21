using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace xIoC.Specificaton.Test.Data
{
    public class CycledSecondService : DisposableComponent, ISecondService
    {
        public CycledSecondService(ICompositionService c)
        {

        }
    }
}