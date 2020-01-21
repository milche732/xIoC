using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using xIoC.Mvc;

namespace xIoC.Mvc4
{
    public class xIoCDependencyResolver : IDependencyResolver
    {
        private readonly static string CONTEXT_XIOC_CONTAINER = "CONTEXT_XIOC_CONTAINER";
        public IContainer RootContainer { get; }

        public xIoCDependencyResolver(IContainer container)
        {
            RootContainer = container ?? throw new ArgumentNullException(nameof(container));
            ApplicationRequestHooker.AssignContainer(this);
        }


        public object GetService(Type serviceType)
        {
            try
            {
                return CurrentContextResolver.Resolve(serviceType);
            }
            catch
            {
                return null;
            }
        }

        public void End()
        {
            CurrentContextResolver.Dispose();
        }

        IContainer CurrentContextResolver
        {
            get
            {
                if (HttpContext.Current.Items[CONTEXT_XIOC_CONTAINER] == null)
                {
                    HttpContext.Current.Items[CONTEXT_XIOC_CONTAINER] = RootContainer.NewScope();
                }


                return (IContainer)HttpContext.Current.Items[CONTEXT_XIOC_CONTAINER];
            }
        }

        /// <summary>
        /// Just return empty List<ServiceType>
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            var enumerableServiceType = typeof(List<>).MakeGenericType(serviceType);
            var instance = Activator.CreateInstance(enumerableServiceType);
            return (IEnumerable<object>)instance;
        }
    }

}
