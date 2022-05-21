using Microsoft.Extensions.Hosting;
using Quartz.Impl;
using System.Threading.Tasks;
using Quartz;
using JobsApp.Jobs;

namespace JobsApp
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            
            StdSchedulerFactory factory = new StdSchedulerFactory();
            IScheduler scheduler = await factory.GetScheduler();
            await scheduler.Start();
            IJobDetail job = JobBuilder.Create<KlineJob>()
                .WithIdentity("job1", "group1")
                .Build();
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInMinutes(5)
                    .RepeatForever())
                .Build();
            await scheduler.ScheduleJob(job, trigger);

        }
       
    }
}
