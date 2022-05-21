using Bybit.Net.Objects.Models;
using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ByBit_Exchange_API.Interfaces
{
    public interface IgetBalance
    {
        public Task<WebCallResult<Dictionary<string, BybitBalance>>> getBalanceAsync();
    }
}
