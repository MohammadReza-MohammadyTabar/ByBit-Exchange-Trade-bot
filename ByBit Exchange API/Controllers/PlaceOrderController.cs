using Bybit.Net.Objects.Models;
using ByBit_Exchange_API.Interfaces;
using CryptoExchange.Net.Objects;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ByBit_Exchange_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlaceOrderController : ControllerBase
    {
        private static IPlaceOrder _placeOrder;
        
        public PlaceOrderController(IPlaceOrder placeOrder)
        {
            
            _placeOrder = placeOrder;
        }
        [HttpGet("{symbol}/{orderSide:bool}/{stopLoss:decimal}/{takeProfit:decimal}/{quantity:decimal}/{reduceOnly?}")]
        public Task<WebCallResult<BybitUsdPerpetualOrder>> PlaceOrder(string symbol,bool orderSide, decimal stopLoss, decimal takeProfit, decimal quantity, bool reduceOnly )
        {
            var result = _placeOrder.placeOrderAsync(symbol,orderSide, stopLoss, takeProfit, quantity, reduceOnly);
            return result;
        }

    }
}
