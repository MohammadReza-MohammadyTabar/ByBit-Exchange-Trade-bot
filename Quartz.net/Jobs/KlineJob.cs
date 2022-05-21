using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Quartz.net.Jobs
{
    public class KlineJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            using (HttpClient client = new HttpClient())
            {
                var result = await client.GetAsync("https://localhost:44306/api/WeatherForecast");
                Console.WriteLine("done");
            }
        }
    }
}
