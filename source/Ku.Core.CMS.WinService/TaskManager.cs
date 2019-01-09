using Ku.Core.CMS.IService.System;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;

namespace Ku.Core.CMS.WinService
{
    public class TaskManager
    {
        private readonly ITimedTaskService _service;
        private readonly IServiceProvider _provider;

        private static StdSchedulerFactory _factory;
        private static IScheduler _scheduler;

        private static List<Assembly> assemblies;

        public TaskManager(ITimedTaskService service, IServiceProvider provider)
        {
            _service = service;
            _provider = provider;
        }

        /// <summary>
        /// 初始化并启动
        /// </summary>
        public void Startup()
        {
            Task.Run(async () => {
                NameValueCollection props = new NameValueCollection
                {
                    { "quartz.serializer.type", "binary" }
                };
                _factory = new StdSchedulerFactory(props);
                _scheduler = await _factory.GetScheduler();
                _scheduler.Context.Add("IServiceProvider", _provider);
                await _scheduler.Start();

                await LoadJobsAsync();
            }); 
        }

        /// <summary>
        /// 加载Jobs
        /// </summary>
        /// <returns></returns>
        public async Task LoadJobsAsync()
        {
            //读取数据库中所有定时任务
            var tasks = await _service.GetListAsync(new Domain.Entity.System.TimedTaskSearch { IsDeleted = false }, null);

            //加载程序集
            assemblies = tasks.Select(x => x.AssemblyName).Distinct().Select(name => AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(name))).ToList();

            foreach (var task in tasks.Where(x => x.Status != Domain.Enum.System.EmTimedTaskStatus.Disable && x.Status != Domain.Enum.System.EmTimedTaskStatus.Expired))
            {
                //处理过期任务
                if (task.ValidEndTime < DateTime.Now)
                {
                    await _service.UpdateTaskStatusAsync(task.Id, new
                    {
                        Status = Domain.Enum.System.EmTimedTaskStatus.Expired
                    });
                    continue;
                }

                var assembly = assemblies.FirstOrDefault(x => x.FullName.Split(',')[0].Equals(task.AssemblyName));
                if (assembly == null)
                {
                    //未正确加载程序集
                    await _service.UpdateTaskStatusAsync(task.Id, new
                    {
                        Status = Domain.Enum.System.EmTimedTaskStatus.Abnormal
                    });
                    continue;
                }

                var type = assembly.DefinedTypes.FirstOrDefault(x => x.FullName.Equals(task.TypeName));
                if (type == null)
                {
                    //未正确加载类型
                    await _service.UpdateTaskStatusAsync(task.Id, new
                    {
                        Status = Domain.Enum.System.EmTimedTaskStatus.Abnormal
                    });
                    continue;
                }

                // 定义一个 Job
                IJobDetail job = JobBuilder.Create(type)
                    .WithIdentity(task.Name, task.Group)
                    .UsingJobData("TaskId", task.Id)
                    .Build();

                // 定义一个触发器
                TriggerBuilder builder = TriggerBuilder.Create().WithIdentity(task.Name, task.Group);
                if (task.ValidStartTime <= DateTime.Now)
                {
                    builder = builder.StartNow();
                }
                else
                {
                    builder = builder.StartAt(task.ValidStartTime);
                }
                builder = builder.EndAt(task.ValidEndTime);

                //Cron
                builder = builder.WithCronSchedule(task.Cron);

                ITrigger trigger = builder.Build();

                //加入Job
                await _scheduler.ScheduleJob(job, trigger);
            }
        }
    }
}
