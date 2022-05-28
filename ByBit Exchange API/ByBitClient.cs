using Bybit.Net.Clients;
using Bybit.Net.Objects;
using ByBit_Exchange_API.Interfaces;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ByBit_Exchange_API
{
    public class ByBitClient : IByBitClient
    {
        public string apikey = Settings.items.apikey;
        public string apisecret = Settings.items.apisecret;
        public BybitClient RegisterClient()
        {
            BybitClient.SetDefaultOptions(new BybitClientOptions
            {
                LogLevel = LogLevel.Information,
                ApiCredentials = new ApiCredentials(apikey, apisecret),
            });

            BybitClient client = new BybitClient(new BybitClientOptions
            {
                LogWriters = new List<ILogger> { new DebugLogger() },
                UsdPerpetualApiOptions = new RestApiClientOptions
                {
                    BaseAddress = BybitApiAddresses.TestNet.UsdPerpetualRestClientAddress,

                }
            });
            return client;
        }
    }
}
