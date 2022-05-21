using ByBit_Exchange_API.Interfaces;
using CryptoExchange.Net.Objects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ByBit_Exchange_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CancelOrderController : ControllerBase
    {
        private static ICancelOrder _cancelOrder;
        public CancelOrderController(ICancelOrder cancelOrder)
        {
            _cancelOrder = cancelOrder;
        }
        [HttpGet]
        public Task<WebCallResult<IEnumerable<string>>> CancelOrder()
        {
            return _cancelOrder.CancelAllOrders("DYDXUSDT");
        }
        

    }
}
