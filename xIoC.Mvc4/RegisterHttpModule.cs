using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using System;
using System.Collections.Generic;
using System.Text;

namespace xIoC.Mvc
{
    public static class RegisterHttpModule
    {
        private static bool _isRegistered;

        public static void Register()
        {
            if (_isRegistered) return;

            _isRegistered = true;
            DynamicModuleUtility.RegisterModule(typeof(ApplicationRequestHooker));
        }
    }
}
