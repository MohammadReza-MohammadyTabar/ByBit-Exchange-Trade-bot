using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Quartz.Impl;
using Quartz.net.Jobs;
using System;
using System.Threading.Tasks;

namespace Quartz.net
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            
            CreateHostBuilder(args).Build().Run();
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

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
