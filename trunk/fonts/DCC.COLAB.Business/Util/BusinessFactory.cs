using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using DCC.COLAB.Business.Infrastructure;
using COLAB.DCC.Business.Util;

namespace DCC.COLAB.Business.Util
{
    public class BusinessFactory
    {
        private static BusinessFactory instance;

        public static BusinessFactory GetInstance()
        {
            if (instance == null)
            {
                instance = new BusinessFactory();
            }

            return instance;
        }

        private BusinessFactory()
        {
            Assembly asm = Assembly.GetAssembly(this.GetType());

            foreach (Type type in asm.GetTypes())
            {
                var tipoBase = type.BaseType;
                if (type != null && tipoBase != null)
                {
                    if (type.BaseType.Name.Equals(typeof(BaseBusinessFacade<>).Name)
                        //|| type.BaseType.Name.Equals(typeof(BaseBusinessFacade<>).Name)
                        && !type.IsAbstract)
                    {
                        if (!ContainerManager.Instance.IsRegistered(type))
                        {
                            ContainerManager.Instance.RegisterType(type,
                                new TransientLifetimeManager(),
                                new Interceptor<VirtualMethodInterceptor>(),
                                new InterceptionBehavior<BusinessMethodManagementBehavior>());
                        }
                    }
                }
            }
        }

        public T Get<T>()
        {
            return ContainerManager.Instance.Get<T>();
        }
    }
}
