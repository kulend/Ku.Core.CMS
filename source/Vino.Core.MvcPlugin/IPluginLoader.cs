using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Ku.Core.MvcPlugin
{
    public interface IPluginLoader
    {
        void LoadEnablePlugins(Action<IPluginStartup> onLoading, Action<Assembly> onLoaded);

        IEnumerable<PluginInfo> GetPlugins();

        void DisablePlugin(string pluginId);

        void EnablePlugin(string pluginId);

        IEnumerable<Assembly> GetPluginAssemblies();

        string PluginFolderName();
    }
}
