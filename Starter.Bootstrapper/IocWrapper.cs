using System;
using System.Collections.Generic;

using Unity;

namespace Starter.Bootstrapper
{
    public class IocWrapper
    {
        public IUnityContainer Container;

        private IocWrapper() { }

        public IocWrapper(IUnityContainer container)
        {
            Container = container;
        }

        /// <summary>
        /// Gets the instance of the dependency resolver
        /// </summary>
        public static IocWrapper Instance
        {
            get
            {
                if (_instance != null)
                {
                    lock (SyncObject)
                    {
                        return _instance;
                    }
                }

                lock (SyncObject)
                {
                    if (_instance != null)
                    {
                        return _instance;
                    }

                    _instance = new IocWrapper();
                }

                return _instance;
            }
            set
            {
                lock (SyncObject)
                {
                    _instance = value;
                }
            }
        }

        public T GetService<T>() where T : class
        {
            return Instance.Container.Resolve<T>();
        }

        public IEnumerable<T> GetServices<T>()
        {
            return Instance.Container.ResolveAll<T>();
        }

        public object GetService(Type serviceType)
        {
            return Instance.Container.Resolve(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return Instance.Container.ResolveAll(serviceType);
        }

        private static readonly object SyncObject = new object();

        private static IocWrapper _instance;
    }
}
