using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;


namespace Quartz.net
{
    public class klinet
    {
        public async Task<HttpResponseMessage> kline()
        {
            using (HttpClient client = new HttpClient())
            {
                //var result = await client.GetAsync("http://localhost:44306/WeatherForecast/GetTest");
                return await client.GetAsync("http://localhost:44306/api/WeatherForecast"); ;
                Console.WriteLine("done");
            }
        }
    }
}
