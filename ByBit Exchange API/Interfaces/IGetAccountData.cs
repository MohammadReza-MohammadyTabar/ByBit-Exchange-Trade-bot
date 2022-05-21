using Bybit.Net.Objects.Models;
using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ByBit_Exchange_API.Interfaces
{
    public interface IGetAccountData
    {
        public Task<WebCallResult<Dictionary<string, BybitBalance>>> getBalanceAsync();
        public Task<WebCallResult<IEnumerable<BybitPositionUsd>>> GetPositionsAsync();
        public Task<WebCallResult<IEnumerable<BybitPositionUsd>>> GetPositionAsync(string symbol);
    }
}
