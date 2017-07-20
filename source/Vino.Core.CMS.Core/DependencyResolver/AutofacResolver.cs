using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;

namespace Vino.Core.CMS.Core.DependencyResolver
{
    public class AutofacResolver : IIocResolver
    {
        private IComponentContext _context;

        public AutofacResolver(IComponentContext context)
        {
            this._context = context;
        }

        public IEnumerable<T> ResolveAll<T>()
        {
            return this._context.Resolve<IEnumerable<T>>().ToList();
        }

        public T Resolve<T>()
        {
            return this._context.Resolve<T>();
        }

        public T Resolve<T>(IDictionary<string, object> withParameters)
        {
            if (withParameters != null && withParameters.Count > 0)
            {
                var pms = new List<NamedParameter>();

                foreach (var item in withParameters)
                {
                    pms.Add(new NamedParameter(item.Key, item.Value));
                }

                return _context.Resolve<T>(pms);
            }

            return this.Resolve<T>();
        }

        public bool IsRegister<T>()
        {
            return _context.IsRegistered<T>();
        }
    }
}
