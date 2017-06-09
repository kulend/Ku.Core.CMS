using System;
using System.Collections.Generic;
using System.Text;

namespace Ku.Core.CMS.Core.DependencyResolver
{
    public interface IDependencyResolverFactory
    {
        /// <summary>
        /// Create dependency resolver
        /// </summary>
        /// <returns>Dependency resolver</returns>
        IDependencyResolver CreateInstance();
    }
}
