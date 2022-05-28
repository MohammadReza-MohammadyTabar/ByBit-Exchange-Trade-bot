using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ByBit_Exchange_API
{
    public static class Settings
    {
        readonly static StreamReader r = new StreamReader("apiJson.json");
        readonly static string json = r.ReadToEnd();
        public static Item items = JsonConvert.DeserializeObject<Item>(json);
    }
    public class Item
    {
        public string port;
        public decimal lagvrage;
        public int STlookbackPeriods;
        public double STmultiplier;
        public string toEmailAddress;
        public string apikey;
        public string apisecret;
    }
}
