using System;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace xIoC.Mvc
{
    public static class ContainerBuilderExtension
    {
        public static void RegisterMvcControllers(this ContainerBuilder cb, Assembly assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException($"Parameter {nameof(assembly)} is null");

            Type controllerType = typeof(Controller);

            foreach (Type controller in assembly.GetTypes().Where(t => t.IsSubclassOf(controllerType) && !t.IsAbstract))
            {
                cb.Register(controller);
            }
        }
    }
}
