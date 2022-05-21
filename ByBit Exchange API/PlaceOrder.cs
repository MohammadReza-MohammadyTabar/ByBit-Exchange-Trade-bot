using ByBit_Exchange_API.Interfaces;
using System;
using System.Threading.Tasks;
using Bybit.Net.Objects.Models;
using CryptoExchange.Net.Objects;

namespace ByBit_Exchange_API
{
    public class PlaceOrder : IPlaceOrder
    {

        private static IByBitClient _client;
        public PlaceOrder(IByBitClient bybitClient)
        {
            _client = bybitClient;

        }
        public async Task<WebCallResult<BybitUsdPerpetualOrder>> placeOrderAsync(bool orderSide, decimal stopLoss, decimal takeProfit, decimal quantity, bool reduceOnly = false)
        {


            var _orderside = Bybit.Net.Enums.OrderSide.Buy;
            if (orderSide == false)
                _orderside = Bybit.Net.Enums.OrderSide.Sell;

            var result = await _client.RegisterClient().UsdPerpetualApi.Trading.PlaceOrderAsync(
                    "DYDXUSDT",
                    _orderside,
                    Bybit.Net.Enums.OrderType.Market,
                    quantity:quantity,
                    Bybit.Net.Enums.TimeInForce.FillOrKill,
                    reduceOnly,
                    stopLossPrice: stopLoss,
                    takeProfitPrice: takeProfit,
                    closeOnTrigger: false   
                    ); 
            return result;
            //var result = await _client.RegisterClient().UsdPerpetualApi.Trading.PlaceConditionalOrderAsync(
            //    symbol: "DYDXUSDT",
            //    Bybit.Net.Enums.OrderSide.Buy,
            //    Bybit.Net.Enums.OrderType.Limit,
            //    50,
            //    price,
            //    tprice,
            //    Bybit.Net.Enums.TimeInForce.GoodTillCanceled,
            //    closeOnTrigger: false,
            //    reduceOnly: false
            //    );
            
        }
    }
}
