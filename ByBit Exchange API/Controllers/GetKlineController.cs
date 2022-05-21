using Bybit.Net.Objects.Models;
using ByBit_Exchange_API.Interfaces;
using CryptoExchange.Net.Objects;
using Microsoft.AspNetCore.Mvc;
using Skender.Stock.Indicators;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
namespace ByBit_Exchange_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetKlineController : ControllerBase
    {
        private static IGetKline _getKline;
        public GetKlineController(IGetKline getKline)
        {
            _getKline = getKline;
            
        }

        [HttpGet("getKline")]
        public Task<IEnumerable<SuperTrendResult>> getKline()
        {
            
            var kline = _getKline.getKlineAsync();            
            return kline;
        }
       
    }
}
