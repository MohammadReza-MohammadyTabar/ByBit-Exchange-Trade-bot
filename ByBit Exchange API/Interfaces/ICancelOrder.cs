using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ByBit_Exchange_API.Interfaces
{
    public interface ICancelOrder
    {
        public Task<WebCallResult<IEnumerable<string>>> CancelAllOrders(string symbol);
    }
}
