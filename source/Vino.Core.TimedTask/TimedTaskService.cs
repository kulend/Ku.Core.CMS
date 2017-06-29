using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public static Dictionary<string, bool> TaskStatus { get; private set; } = new Dictionary<string, bool>();
        public static Dictionary<string, Timer> StaticTimers { get; private set; } = new Dictionary<string, Timer>();

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
                            var timerId = new Guid().ToString();
                            var timer = new Timer(t =>
                            {
                                Execute(timerId, clazz, method, invoke);
                            }, null, delta, invoke.AutoReset ? invoke.Interval : 0);
                            StaticTimers.Add(timerId, timer);
                        });
                    }
                }
            }
        }

        public bool Execute(string timerId, TypeInfo clazz, MethodInfo method, InvokeAttribute invoke)
        {
            var identifier = clazz.FullName + "." + method.Name;

            if (invoke != null && invoke.ExpireTime != default(DateTime)
                && invoke.ExpireTime <= DateTime.Now)
            {
                //已过期失效
                StaticTimers[timerId].Dispose();
                TaskStatus[timerId] = false;
                StaticTimers.Remove(timerId);
                TaskStatus.Remove(timerId);
                return false;
            }
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

            var taskAttr = clazz.GetCustomAttribute<VinoTimedTaskAttribute>();
            var taskName = (taskAttr != null && !string.IsNullOrEmpty(taskAttr.Name)) ? taskAttr.Name : clazz.Name;
            var singleTaskAttr = method.GetCustomAttribute<SingleTaskAttribute>(true);
            lock (this)
            {
                if (singleTaskAttr != null && singleTaskAttr.IsSingleTask 
                    && TaskStatus.ContainsKey(timerId) && TaskStatus[timerId])
                {
                    return false;
                }
                TaskStatus[timerId] = true;
            }
            try
            {
                logger?.LogInformation($"[事务]{taskName} 开始执行...");
                Debug.WriteLine($"[事务]{taskName} 开始执行...");
                Stopwatch sw = new Stopwatch();
                sw.Start();
                method.Invoke(job, paramtypes);
                sw.Stop();
                logger?.LogInformation($"[事务]{taskName} 执行结束，耗时{sw.ElapsedMilliseconds}毫秒。");
                Debug.WriteLine($"[事务]{taskName} 执行结束，耗时{sw.ElapsedMilliseconds}毫秒。");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
            }
            TaskStatus[timerId] = false;
            return true;
        }
    }
}
