using ByBit_Exchange_API.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bybit.Net.Objects.Models;
using CryptoExchange.Net.Objects;


namespace ByBit_Exchange_API
{
    public class getAccountData : IGetAccountData
    {
        private static IByBitClient _client;
        
        public getAccountData(IByBitClient bybitClient)
        {
            _client = bybitClient;
            
        }
        public async Task<WebCallResult<Dictionary<string, BybitBalance>>> getBalanceAsync()
        {

            
            return await _client.RegisterClient().UsdPerpetualApi.Account.GetBalancesAsync();
        }

        public async  Task<WebCallResult<IEnumerable<BybitPositionUsd>>> GetPositionAsync(string symbol)
        {
            return await _client.RegisterClient().UsdPerpetualApi.Account.GetPositionAsync(symbol);
        }

        public async Task<WebCallResult<IEnumerable<BybitPositionUsd>>> GetPositionsAsync()
        {

            return await _client.RegisterClient().UsdPerpetualApi.Account.GetPositionsAsync();
        }

    }
}
