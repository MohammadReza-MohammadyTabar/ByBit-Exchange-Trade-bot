using Bybit.Net.Objects.Models;
using CryptoExchange.Net.Objects;
using Skender.Stock.Indicators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ByBit_Exchange_API.Interfaces
{
    public interface IGetKline
    {
        public Task<IEnumerable<SuperTrendResult>> getKlineAsync();
    }
}
