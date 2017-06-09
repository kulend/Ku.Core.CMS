using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Ku.Core.CMS.Core.DependencyResolver
{
    public static class IoC
    {
        #region Fields

        private static IDependencyResolver _resolver;

        #endregion

        #region Methods

        public static IServiceProvider InitializeWith(IDependencyResolverFactory factory, IServiceCollection services)
        {
            if (factory == null)
                throw new ArgumentNullException("factory");

            _resolver = factory.CreateInstance();
            return _resolver.InitDependencyResolver(services);
        }

        public static void Register<T>(T instance)
        {
            if (instance == null)
                throw new ArgumentNullException("instance");

            _resolver.Register(instance);
        }

        public static void Inject<T>(T existing)
        {
            if (existing == null)
                throw new ArgumentNullException("existing");

            _resolver.Inject(existing);
        }

        public static T Resolve<T>(Type type)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            return _resolver.Resolve<T>(type);
        }

        public static T Resolve<T>(Type type, string name)
        {
            if (type == null)
                throw new ArgumentNullException("type");
            if (name == null)
                throw new ArgumentNullException("name");

            return _resolver.Resolve<T>(type, name);
        }

        public static T Resolve<T>()
        {
            return _resolver.Resolve<T>();
        }

        public static T Resolve<T>(string name)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");

            return _resolver.Resolve<T>(name);
        }

        public static object Resolve(Type type)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            return _resolver.Resolve(type);
        }

        public static IEnumerable<T> ResolveAll<T>()
        {
            return _resolver.ResolveAll<T>();
        }

        public static object GetContainer()
        {
            return _resolver.GetContainer();
        }

        #endregion
    }
}
