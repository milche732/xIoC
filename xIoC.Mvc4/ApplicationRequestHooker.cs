using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using xIoC;
using xIoC.Mvc4;

namespace xIoC.Mvc
{
    public class ApplicationRequestHooker : IHttpModule
    {
        private static xIoCDependencyResolver container;
        public void Dispose()
        {

        }

        public static void AssignContainer(xIoCDependencyResolver c)
        {
            container = c;
        }

        public void Init(HttpApplication context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            context.EndRequest += Context_EndRequest;
        }

        private void Context_EndRequest(object sender, EventArgs e)
        {
            container.End();
        }
    }
}
