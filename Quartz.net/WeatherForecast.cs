using Quartz.Impl;
using Quartz.net.Jobs;
using System;
using System.Threading.Tasks;

namespace Quartz.net
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; }


       public async Task<DateTimeOffset> testc()
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
            
            return await scheduler.ScheduleJob(job, trigger);
        }
    }
}
