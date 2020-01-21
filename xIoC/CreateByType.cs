using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace xIoC
{
    class CreateByType : IContractActivator
    {
        private readonly Type _targetType;

        public CreateByType(Type targetType)
        {
            _targetType = targetType;
        }
        public object CreateInstance(IResolve resolvingContext)
        {
            ConstructorInfo[] ctors = _targetType.GetConstructors();
            if (ctors.Length > 1) throw new Exception($"type {_targetType} has more than 1 constructor");

            object instance;

            if (ctors.Length == 0)
            {
                instance = Activator.CreateInstance(_targetType);
            }
            else
            {
                ParameterInfo[] parameters = ctors[0].GetParameters();
                object[] instances = new object[parameters.Length];

                for (int i = 0; i < parameters.Length; i++)
                {
                    instances[i] = resolvingContext.Resolve(parameters[i].ParameterType);
                };

                instance = Activator.CreateInstance(_targetType, instances);
            }
            return instance;
        }
    }
}
