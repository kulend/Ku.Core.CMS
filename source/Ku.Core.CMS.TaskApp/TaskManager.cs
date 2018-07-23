﻿using Ku.Core.CMS.IService.System;
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

namespace Ku.Core.CMS.TaskApp
{
    public class TaskManager
    {
        private readonly ITimedTaskService _service;

        private static StdSchedulerFactory _factory;
        private static IScheduler _scheduler;

        private List<Assembly> assemblies;

        public TaskManager(ITimedTaskService service)
        {
            _service = service;
        }

        public void Startup()
        {
            Task.Run(async () => {
                // Grab the Scheduler instance from the Factory
                NameValueCollection props = new NameValueCollection
                {
                    { "quartz.serializer.type", "binary" }
                };
                _factory = new StdSchedulerFactory(props);
                _scheduler = await _factory.GetScheduler();

                await _scheduler.Start();

                //读取数据库中所有定时任务
                var tasks = await _service.GetListAsync(new Domain.Entity.System.TimedTaskSearch { IsDeleted = false}, null);

                //加载程序集
                assemblies = tasks.Select(x => x.AssemblyName).Distinct().Select(name => AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(name))).ToList();

                foreach (var task in tasks)
                {
                    var assembly = assemblies.FirstOrDefault(x => x.FullName.Split(',')[0].Equals(task.AssemblyName));
                    if (assembly == null)
                    {
                        //未正确加载程序集
                        continue;
                    }

                    var type = assembly.DefinedTypes.FirstOrDefault(x => x.FullName.Equals(task.TypeName));
                    if (type == null)
                    {
                        //未正确加载类型
                        continue;
                    }


                    // 定义一个 Job
                    IJobDetail job = JobBuilder.Create(type)
                        .WithIdentity(task.Name, task.Group)
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
            }); 
        }
    }
}