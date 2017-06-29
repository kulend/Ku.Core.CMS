using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text;

namespace Vino.Core.TimedTask.Common
{
    public interface IAssemblyLocator
    {
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Might be expensive.")]
        IList<Assembly> GetAssemblies();
    }
}
