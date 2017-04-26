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

        private readonly IScheduler scheduler;

        private ScheduledManager()
        {
            scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();
        }

        public void Refresh<T>(IList<TaskModel> tasks) where T : IJob
        {
            foreach (var task in tasks)
            {
                Refresh<T>(task);
            }
        }

        public void Refresh<T>(TaskModel task) where T : IJob
        {
            var job = CreateJob<T>(task);
            var trigger = CreateTrigger(task);
            AssociationJob(job, trigger);
        }

        public void Close()
        {
            scheduler.Shutdown();
        }

        public void Clear()
        {
            if (!scheduler.IsShutdown)
            {
                scheduler.PauseAll();
                scheduler.Clear();
            }
        }

        public void AssociationJob(IJobDetail job, ITrigger trigger)
        {
            if (job != null && trigger != null)
            {
                scheduler.DeleteJob(job.Key);
                scheduler.ScheduleJob(job, trigger);
            }
        }

        public IJobDetail CreateJob<T>(IJobEntity entity, JobDataMap map = null) where T : IJob
        {
            if (entity == null)
                return null;
            map = map ?? new JobDataMap();
            var job = JobBuilder.Create<T>()
                .WithIdentity(entity.id, entity.Group)
                .UsingJobData(map)
                .Build();
            return job;
        }

        public ITrigger CreateTrigger(ITriggerEntity entity, JobDataMap map = null)
        {
            if (entity == null)
                return null;
            map = map ?? new JobDataMap();
            ITrigger trigger = null;
            if (!string.IsNullOrEmpty(entity.expression) && CronExpression.IsValidExpression(entity.expression))
            {
                trigger = TriggerBuilder.Create()
                    .WithIdentity(entity.id, entity.Group)
                    .UsingJobData(map)
                    .WithCronSchedule(entity.expression)
                    .Build();
            }
            else
            {
                trigger = TriggerBuilder.Create()
                    .WithIdentity(entity.id, entity.Group)
                    .Build();
            }

            return trigger;
        }
    }
}