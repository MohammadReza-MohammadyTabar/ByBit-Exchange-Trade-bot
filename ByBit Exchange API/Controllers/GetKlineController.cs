using ByBit_Exchange_API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Skender.Stock.Indicators;
using System.Collections.Generic;
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

        [HttpGet("getKline/{symbol?}/{quantity?}")]
        public Task<IEnumerable<SuperTrendResult>> GetTest(string symbol="DYDXUSDT",decimal quantity=50)
        {
            
            var kline = _getKline.getKlineAsync(symbol,quantity);            
            return kline;
        }
       
    }
}
