using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Vino.Core.TimedTask.Common;
using Microsoft.Extensions.DependencyInjection;
using Vino.Core.TimedTask.Attribute;

namespace Vino.Core.TimedTask
{
    public class TimedTaskService
    {
        private ILogger logger { get; set; }

        private IAssemblyLocator locator { get; set; }
        private IServiceProvider services { get; set; }
        private List<TypeInfo> JobTypeCollection { get; set; } = new List<TypeInfo>();

        public Dictionary<string, bool> TaskStatus { get; private set; } = new Dictionary<string, bool>();
        private readonly List<Timer> StaticTimers = new List<Timer>();

        public TimedTaskService(IAssemblyLocator locator, IServiceProvider services)
        {
            this.services = services;
            this.locator = locator;
            this.logger = services.GetService<ILogger>();
            var asm = locator.GetAssemblies();
            foreach (var x in asm)
            {
                //查找带有VinoTimedTaskAttribute的类
                var types = x.DefinedTypes.Where(y => y.GetCustomAttribute(typeof(VinoTimedTaskAttribute), true) != null);
                foreach (var type in types)
                {
                    JobTypeCollection.Add(type);
                }
            }

            //取得所有方法
            foreach (var clazz in JobTypeCollection)
            {
                foreach (var method in 
                    clazz.DeclaredMethods.Where(x=>x.GetCustomAttributes<InvokeAttribute>(true).Any()))
                {
                    //取得所有Invoke配置
                    var invokes = method.GetCustomAttributes<InvokeAttribute>(true);
                    foreach (var invoke in invokes.Where(i=>i.IsEnabled 
                        && (i.ExpireTime >= DateTime.Now || i.ExpireTime == default(DateTime))))
                    {
                        //需要延时的时间
                        int delta = 0;
                        if (invoke.BeginTime == default(DateTime))
                        {
                            invoke.BeginTime = DateTime.Now;
                        }
                        else
                        {
                            delta = Convert.ToInt32((invoke.BeginTime - DateTime.Now).TotalMilliseconds);
                        }
                        if (delta < 0)
                        {
                            delta = delta % invoke.Interval;
                            if (delta < 0)
                                delta += invoke.Interval;
                        }
                        Task.Factory.StartNew(() =>
                        {
                            var timer = new Timer(t => {
                                Execute(clazz, method, invoke);
                            }, null, delta, invoke.Interval);
                            StaticTimers.Add(timer);
                        });
                    }
                }
            }
        }

        public bool Execute(TypeInfo clazz, MethodInfo method, InvokeAttribute invoke)
        {
            var argtypes = clazz.GetConstructors()
                .First()
                .GetParameters()
                .Select(x =>
                {
                    if (x.ParameterType == typeof(IServiceProvider))
                        return services;
                    else
                        return services.GetService(x.ParameterType);
                }).ToArray();

            var job = Activator.CreateInstance(clazz.AsType(), argtypes);
            var paramtypes = method.GetParameters().Select(x => services.GetService(x.ParameterType)).ToArray();

            var identifier = clazz.FullName + "." + method.Name;

            var singleTaskAttr = method.GetCustomAttribute<SingleTaskAttribute>(true);
            lock (this)
            {
                if (singleTaskAttr != null && singleTaskAttr.IsSingleTask 
                    && TaskStatus.ContainsKey(identifier) && TaskStatus[identifier])
                {
                    return false;
                }
                TaskStatus[identifier] = true;
            }
            try
            {
                logger?.LogInformation($"Invoking {identifier} ...");
                method.Invoke(job, paramtypes);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
            }
            TaskStatus[identifier] = false;
            return true;
        }
    }
}
