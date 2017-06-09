using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.Loader;

namespace Ku.Core.CMS.Core.DependencyResolver
{
    public class AutofacDependencyResolver : IDependencyResolver
    {
        private IContainer _container;
        private ContainerBuilder _builder;

        public AutofacDependencyResolver()
        {
        }

        public IServiceProvider InitDependencyResolver(IServiceCollection services)
        {
            _builder = new ContainerBuilder();

            var assemblys = new string[] { "Ku.BLL"};
            var loadedAssemblys = new List<Assembly>();
            foreach (var ass in assemblys)
            {
                try
                {
                    var assembly = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(ass));
                    loadedAssemblys.Add(assembly);
                }
                catch
                {
                }
            }
            Type iDependencyType = typeof(IDependency);
            foreach (var ass in loadedAssemblys)
            {
                _builder.RegisterAssemblyTypes(ass)
                    .Where(type => iDependencyType.IsAssignableFrom(type) && !type.GetTypeInfo().IsAbstract)
                    .AsSelf()
                    .AsImplementedInterfaces()
                    .PropertiesAutowired()
                    .SingleInstance();
            }

            //IConfigurationBuilder config = new ConfigurationBuilder();
            //var path = Directory.GetCurrentDirectory();
            //IConfigurationSource autofacJsonConfigSource = new JsonConfigurationSource()
            //{
            //    Path = path + "\\config\\autofac.json",
            //    Optional = false,
            //    ReloadOnChange = false,
            //};
            //config.Add(autofacJsonConfigSource);

            //var module = new ConfigurationModule(config.Build());
            //_builder.RegisterModule(module);

            _builder.Populate(services);
            _container = _builder.Build();
            return new AutofacServiceProvider(_container);
        }

        public void Register<T>(T instance)
        {
            throw new NotImplementedException();
        }

        public void Inject<T>(T existing)
        {
            throw new NotImplementedException();
        }

        public T Resolve<T>(Type type)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            return (T)_container.Resolve(type);
        }

        public T Resolve<T>(Type type, string name)
        {
            throw new NotImplementedException();
        }

        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        public T Resolve<T>(string name)
        {
            throw new NotImplementedException();
        }

        public object Resolve(Type type)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            return _container.Resolve(type);
        }

        public IEnumerable<T> ResolveAll<T>()
        {
            throw new NotImplementedException();
        }

        public object GetContainer()
        {
            return _container;
        }
    }
}
