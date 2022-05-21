using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ByBit_Exchange_API.Interfaces;
using CryptoExchange.Net.Objects;

namespace ByBit_Exchange_API
{
    public class CancelOrder : ICancelOrder
    {
        private static IByBitClient _client;
        public CancelOrder(IByBitClient bybitClient)
        {
            _client = bybitClient;

        }

        public async Task<WebCallResult<IEnumerable<string>>> CancelAllOrders(string symbol)
        {
            
            return await _client.RegisterClient().UsdPerpetualApi.Trading.CancelAllConditionalOrdersAsync(symbol);
        }

    }
}
