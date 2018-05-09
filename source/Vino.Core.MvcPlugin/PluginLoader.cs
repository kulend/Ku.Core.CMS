using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Ku.Core.MvcPlugin
{
    public class PluginLoader : IPluginLoader
    {
        private readonly IHostingEnvironment _hostEnvironment;

        public const string PluginFolder = "Plugins";
        private const string _pluginConfigFileName = "plugin.json";

        private static List<AssemblyLoader> Loaders = new List<AssemblyLoader>();
        private static Dictionary<string, Assembly> LoadedAssemblies = new Dictionary<string, Assembly>();

        public PluginLoader(IHostingEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

        public void LoadEnablePlugins(Action<IPluginStartup> onLoading, Action<Assembly> onLoaded)
        {
            GetPlugins().Where(m => m.Enable && !string.IsNullOrEmpty(m.ID)).Each(m =>
            {
                var loader = new AssemblyLoader();
                loader.OnLoading = onLoading;
                loader.OnLoaded = onLoaded;

                var assemblies = loader.LoadPlugin(Path.Combine(m.RelativePath, (_hostEnvironment.IsDevelopment() ? m.DeveloperFileName : m.FileName).ToFilePath()));
                assemblies.Each(assembly =>
                {
                    if (!LoadedAssemblies.ContainsKey(assembly.FullName))
                    {
                        LoadedAssemblies.Add(assembly.FullName, assembly);
                    }
                });
                Loaders.Add(loader);
            });
        }

        public IEnumerable<Assembly> GetPluginAssemblies()
        {
            return LoadedAssemblies.Select(m => m.Value);
        }

        public string PluginFolderName()
        {
            return PluginFolder;
        }

        public void DisablePlugin(string pluginId)
        {
            GetPlugins().Where(m => m.ID == pluginId).Each(m =>
            {
                m.Enable = false;
                File.WriteAllText(m.RelativePath + $"\\{_pluginConfigFileName}", JsonConvert.SerializeObject(m));
            });
        }

        public void EnablePlugin(string pluginId)
        {
            GetPlugins().Where(m => m.ID == pluginId).Each(m =>
            {
                m.Enable = true;
                File.WriteAllText(m.RelativePath + $"\\{_pluginConfigFileName}", JsonConvert.SerializeObject(m));
            });
        }


        public IEnumerable<PluginInfo> GetPlugins()
        {
            string modulePath = _hostEnvironment.IsDevelopment() ?
                new DirectoryInfo(_hostEnvironment.ContentRootPath).Parent.FullName :
                Path.Combine(_hostEnvironment.WebRootPath, PluginFolder);

            if (Directory.Exists(modulePath))
            {
                var modules = new DirectoryInfo(modulePath).GetDirectories();
                foreach (var item in modules)
                {
                    string pluginConfigFile = Path.Combine(item.FullName, _pluginConfigFileName);
                    if (File.Exists(pluginConfigFile))
                    {
                        var plugin = JsonConvert.DeserializeObject<PluginInfo>(File.ReadAllText(pluginConfigFile));
                        plugin.RelativePath = item.FullName;
                        yield return plugin;
                    }
                }
            }

        }
    }
}
