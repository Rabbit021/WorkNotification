using System;
using System.Collections.Generic;
using Alarm.Models;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Matchers;
using Quartz.Spi;

namespace Alarm.CommonLib
{
    public class ScheduledManager
    {
        public static ScheduledManager Instance = new ScheduledManager();

        private IScheduler scheduler;
        private string Group = "Defalut";

        private ScheduledManager()
        {
            scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();
        }

        public void Refresh(IList<TaskModel> tasks)
        {
            Clear();
            foreach (var itr in tasks)
            {
                CreateOrUpdate(itr);
            }
        }

        private void CreateOrUpdate(TaskModel task)
        {
            var map = new JobDataMap();
            map.Add("task", task);

            var job = JobBuilder.Create<ShowAlarmJob>()
                .WithIdentity(task.id, Group)
                .UsingJobData(map)
                .Build();

            ITrigger trigger = null;
            if (!string.IsNullOrEmpty(task.expression))
            {
                trigger = TriggerBuilder.Create()
                    .WithIdentity(job.Key.Name, job.Key.Group)
                    .WithCronSchedule(task.expression)
                    .Build();
            }
            else
            {
                trigger = TriggerBuilder.Create()
                 .WithIdentity(job.Key.Name, job.Key.Group)
                 .Build();
            }

            if (trigger != null)
                scheduler.ScheduleJob(job, trigger);
        }

        public void Close()
        {
            Clear();
        }

        public void Clear()
        {
            scheduler.PauseAll();
            scheduler.Clear();
        }
    }
}