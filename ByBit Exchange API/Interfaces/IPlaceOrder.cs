using Bybit.Net.Objects.Models;
using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ByBit_Exchange_API.Interfaces
{
    public interface IPlaceOrder
    {
        public Task<WebCallResult<BybitUsdPerpetualOrder>> placeOrderAsync(string symbol,bool orderSide, decimal stopLoss, decimal takeProfit, decimal quantity, bool reduceOnly);

    }
}
