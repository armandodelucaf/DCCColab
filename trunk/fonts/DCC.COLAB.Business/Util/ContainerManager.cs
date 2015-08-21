using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace DCC.COLAB.Business.Util
{
    public class ContainerManager
    {
        private static ContainerManager instance;

        private IUnityContainer container;

        public static ContainerManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ContainerManager();
                }

                return instance;
            }
        }

        private ContainerManager()
        {
            container = new UnityContainer().LoadConfiguration();
            container.AddNewExtension<Interception>();
        }

        public T Get<T>()
        {
            if (!container.IsRegistered<T>())
            {
                container.RegisterType<T>(
                new TransientLifetimeManager(),
                new Interceptor<VirtualMethodInterceptor>(),
                new InterceptionBehavior<BusinessMethodManagementBehavior>());
            }
            return container.Resolve<T>();
        }

        public bool IsRegistered(Type type)
        {
            return this.container.IsRegistered(type);
        }

        public void RegisterType(Type type, LifetimeManager transientLifetimeManager, params InjectionMember[] injectionMembers)
        {
            this.container.RegisterType(type, transientLifetimeManager, injectionMembers);
        }
    }
}
