using System;
using System.Collections.Generic;
using System.Text;

namespace Vino.Core.CMS.Core.DependencyResolver
{
    public class DependencyResolverFactory : IDependencyResolverFactory
    {
        #region 字段

        /// <summary>
        /// Resolver type
        /// </summary>
        private readonly Type _resolverType;

        #endregion

        #region .ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public DependencyResolverFactory()
            : this("Vino.Core.CMS.Core.DependencyResolver.AutofacDependencyResolver")
        {
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="resolverTypeName">Resolver type name</param>
        public DependencyResolverFactory(string resolverTypeName)
        {
            if (String.IsNullOrEmpty(resolverTypeName))
            {
                throw new ArgumentNullException("resolverTypeName");
            }

            _resolverType = Type.GetType(resolverTypeName, true, true);
        }

        #endregion

        #region IDependencyResolverFactory 接口实现

        /// <summary>
        /// Create dependency resolver
        /// </summary>
        /// <returns>Dependency resolver</returns>
        public IDependencyResolver CreateInstance()
        {
            return Activator.CreateInstance(_resolverType) as IDependencyResolver;
        }
        #endregion

    }
}
