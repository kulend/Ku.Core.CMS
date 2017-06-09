using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Ku.Core.CMS.Core.DependencyResolver
{
    public interface IDependencyResolver
    {
        IServiceProvider InitDependencyResolver(IServiceCollection services);

        /// <summary>
        /// Register instance
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="instance">Instance</param>
        void Register<T>(T instance);

        /// <summary>
        /// Inject
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="existing">Type</param>
        void Inject<T>(T existing);

        /// <summary>
        /// Resolve
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="type">Type</param>
        /// <returns>Result</returns>
        T Resolve<T>(Type type);

        /// <summary>
        /// Resolve
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="type">Type</param>
        /// <param name="name">Name</param>
        /// <returns>Result</returns>
        T Resolve<T>(Type type, string name);

        /// <summary>
        /// Resolve
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>Result</returns>
        T Resolve<T>();

        /// <summary>
        /// Resolve
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="name">Name</param>
        /// <returns>Result</returns>
        T Resolve<T>(string name);

        /// <summary>
        /// Resolve
        /// </summary>
        /// <param name="type">Type</param>
        /// <returns>Result</returns>
        object Resolve(Type type);

        /// <summary>
        /// Resolve all
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>Result</returns>
        IEnumerable<T> ResolveAll<T>();

        object GetContainer();
    }
}
