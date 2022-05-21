using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JobsApp.Jobs
{
    class KlineJob: IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            using (HttpClient client = new HttpClient())
            {
                var result = await client.GetAsync("http://localhost:44306/WeatherForecast/GetTest");
            }
        }
    }
}
