using System;
using System.Collections.Generic;
using System.Text;
using xIoC.Exceptions;
using xIoC.Specificaton.Test.Data;
using Xunit;

namespace xIoC.Specificaton.Test
{
    public class CyclingReferenceTests
    {
        [Fact]
        public void Resolve_CycledConstructor_ThrowCycleReferenceException() 
        {
            ContainerBuilder cb = new ContainerBuilder();
            cb.Register<IFirstService, FirstService>();
            cb.Register<ISecondService, CycledSecondService>();
            cb.Register<ICompositionService, CompositionService>();

            IContainer root = cb.Build();

            Assert.ThrowsAny<CycleReferenceException>(() => root.Resolve<ICompositionService>());
        }
    }
}
