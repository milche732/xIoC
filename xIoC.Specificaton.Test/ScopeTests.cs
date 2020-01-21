using System;
using xIoC.Specificaton.Test.Data;
using Xunit;

namespace xIoC.Specificaton.Test
{
    public class ScopeTests
    {
        [Fact]
        public void TransientComponentsAreDifferentFromDifferentScopes()
        {
            ContainerBuilder cb = new ContainerBuilder();
            cb.Register<FirstService>();

            using (var c1 = cb.Build())
            {
                var fs1 = c1.Resolve<FirstService>();

                using (var c2 = c1.NewScope())
                {
                    var fs2 = c2.Resolve<FirstService>();

                    Assert.NotEqual(fs1, fs2);
                }
            }


        }

        [Fact]
        public void SingltonComponentsAreTheSameFromDifferentScopes()
        {
            ContainerBuilder cb = new ContainerBuilder();
            cb.Register<FirstService>(LifecycleType.Singleton);

            using (var c1 = cb.Build())
            {
                var fs1 = c1.Resolve<FirstService>();

                using (var c2 = c1.NewScope())
                {
                    var fs2 = c2.Resolve<FirstService>();

                    Assert.Equal(fs1, fs2);
                }
            }
        }
    }
}
