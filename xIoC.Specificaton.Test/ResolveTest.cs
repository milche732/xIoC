using System;
using xIoC.Exceptions;
using xIoC.Specificaton.Test.Data;
using Xunit;

namespace xIoC.Specificaton.Test
{
    public class ResolveTests
    {
        [Fact]
        public void Resolve_SelfRegistered_Successfully()
        {
            ContainerBuilder cb = new ContainerBuilder();
            cb.Register<FirstService>();
            IContainer c = cb.Build();
            
            var fs = c.Resolve<FirstService>();
         
            Assert.NotNull(fs);
        }

        [Fact]
        public void Resolve_TypedContract_Successfully()
        {
            ContainerBuilder cb = new ContainerBuilder();
            cb.Register<IFirstService,FirstService>(LifecycleType.Singleton);
            IContainer c = cb.Build();
            
            var fs = c.Resolve<IFirstService>();

            Assert.IsType<FirstService>(fs);
        }

        [Fact]
        public void Resolve_FactoryContract_Successfully()
        {
            ContainerBuilder cb = new ContainerBuilder();
            cb.Register<IFirstService>((c)=>new FirstService());
            IContainer root = cb.Build();

            var fs = root.Resolve<IFirstService>();

            Assert.IsType<FirstService>(fs);
        }

        [Fact]
        public void Resolve_Composition_Successfully()
        {
            ContainerBuilder cb = new ContainerBuilder();
            cb.Register<IFirstService,FirstService>();
            cb.Register<ISecondService,SecondService>();
            cb.Register<ICompositionService, CompositionService>();
            IContainer root = cb.Build();

            var fs = root.Resolve<ICompositionService>();

            Assert.IsType<CompositionService>(fs);
        }

        [Fact]
        public void Resolve_MissedRegistrationInCompositon_ThrowContractNotFoundException()
        {
            ContainerBuilder cb = new ContainerBuilder();
            cb.Register<IFirstService, FirstService>();
            cb.Register<ICompositionService, CompositionService>();

            IContainer root = cb.Build();

            Assert.ThrowsAny<ContractNotFoundException>(() => root.Resolve<ICompositionService>());
        }

        [Fact]
        public void Resolve_MissedRegistration_ThrowContractNotFoundException()
        {
            ContainerBuilder cb = new ContainerBuilder();
            cb.Register<ISecondService, SecondService>();

            IContainer root = cb.Build();

            Assert.ThrowsAny<ContractNotFoundException>(() => root.Resolve<IFirstService>());
        }
    }
}
