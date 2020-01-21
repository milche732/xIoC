using System;
using xIoC.Specificaton.Test.Data;
using Xunit;

namespace xIoC.Specificaton.Test
{
    public class DisposalTests
    {
        [Fact]
        public void TransientComponentsAreDisposedAfterContainerDisposal()
        {
            ContainerBuilder cb = new ContainerBuilder();
            cb.Register<FirstService>();
            cb.Register<SecondService>();

            IContainer c = cb.Build();
            var fs = c.Resolve<FirstService>();
            var ss = c.Resolve<SecondService>();

            c.Dispose();

            Assert.True(fs.IsDisposed);
            Assert.True(ss.IsDisposed);
        }

        [Fact]
        public void SingltonIsDisposedAfterContainerDisposal()
        {
            ContainerBuilder cb = new ContainerBuilder();
            cb.Register<FirstService>(LifecycleType.Singleton);

            IContainer c = cb.Build();
            var fs = c.Resolve<FirstService>();

            c.Dispose();

            Assert.True(fs.IsDisposed);
        }
        
        [Fact]
        public void SingltonIsNotDisposedAfterSubContainerDisposal()
        {
            ContainerBuilder cb = new ContainerBuilder();
            cb.Register<FirstService>(LifecycleType.Singleton);

            IContainer root = cb.Build();

            IScope child = root.NewScope();

            FirstService fs = child.Resolve<FirstService>();
           
            child.Dispose();
            Assert.False(fs.IsDisposed);
         
            root.Dispose();
            Assert.True(fs.IsDisposed);
        }
    }
}
