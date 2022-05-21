using Bybit.Net.Objects.Models;
using ByBit_Exchange_API.Interfaces;
using CryptoExchange.Net.Objects;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ByBit_Exchange_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GetAccountDataController : ControllerBase
    {
        private static Interfaces.IGetAccountData _getAccountData;
        public GetAccountDataController(Interfaces.IGetAccountData getAccountData)
        {
            _getAccountData = getAccountData;
        }
        [HttpGet("getBalance")]
        public Task<WebCallResult<Dictionary<string, BybitBalance>>> getBalance()
        {
            return _getAccountData.getBalanceAsync();
        }
        [HttpGet("getposition/{symbol}")]
        public Task<WebCallResult<IEnumerable<BybitPositionUsd>>> getPosition(string symbol)
        {
            return _getAccountData.GetPositionAsync(symbol);
        }
        [HttpGet("getPositions")]
        public Task<WebCallResult<IEnumerable<BybitPositionUsd>>> getPositions()
        {
            return _getAccountData.GetPositionsAsync();
        }

    }
}
