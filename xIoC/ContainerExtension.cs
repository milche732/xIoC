using System;
using System.Collections.Generic;
using System.Text;

namespace xIoC
{
    public static class ContainerExtension
    {
        public static T Resolve<T>(this IScope scope)
        {
            object result = scope.Resolve(typeof(T));
            if (result != null)
                return (T)result;

            return default(T);
        }

        public static T Resolve<T>(this IContainer container)
        {
            object result = container.Resolve(typeof(T));
            if (result != null)
                return (T)result;

            return default(T);
        }
    }
}
