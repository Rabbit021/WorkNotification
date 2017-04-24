using System;
using System.Windows;
using Quartz;

namespace Alarm.Models
{
    [DisallowConcurrentExecution]
    public class ShowAlarmJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            var map = context.JobDetail.JobDataMap;
            object rst;
            map.TryGetValue("task", out rst);

            var task = rst as TaskModel;
            if (task != null)
            {
                Console.WriteLine(task.id);
            }
            else
            {
                Console.WriteLine(nameof(ShowAlarmJob));
            }
        }
    }
}