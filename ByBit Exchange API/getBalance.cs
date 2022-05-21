using ByBit_Exchange_API.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bybit.Net.Objects.Models;
using CryptoExchange.Net.Objects;


namespace ByBit_Exchange_API
{
    public class getBalance : IgetBalance
    {
        private static IByBitClient _client;
        public getBalance(IByBitClient bybitClient)
        {
            _client = bybitClient;
            
        }
        public async Task<WebCallResult<Dictionary<string, BybitBalance>>> getBalanceAsync()
        {
            var result = await _client.RegisterClient().UsdPerpetualApi.Account.GetBalancesAsync();
            return result;
        }
        
    }
}
