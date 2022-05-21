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
    public class GetBalanceController : ControllerBase
    {
        private static IgetBalance _getBalance;
        public GetBalanceController(IgetBalance getBalance)
        {
            _getBalance = getBalance;
        }
        [HttpGet]
        public Task<WebCallResult<Dictionary<string, BybitBalance>>> getBalance()
        {
            return _getBalance.getBalanceAsync();
        }
    }
}
