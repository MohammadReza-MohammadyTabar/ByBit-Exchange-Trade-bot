using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Bybit.Net.Objects.Models;
using ByBit_Exchange_API.Interfaces;
using Skender.Stock.Indicators;
using System.Linq;
using System.IO;
using ByBit_Exchange_API.Controllers;

namespace ByBit_Exchange_API
{
    public class getKline : IGetKline
    {
        private static IByBitClient _client;
        private static IPlaceOrder _placeOrder;
        private static PlaceOrderController _placeOrderController;
        private static IGetAccountData _getAccountData;

        public string symbol = "DYDXUSDT";
        public getKline(IByBitClient bybitClient,IPlaceOrder placeOrder, PlaceOrderController placeOrderController,IGetAccountData getAccountData)
        {
            _placeOrderController = placeOrderController;
            _placeOrder = placeOrder;
            _client = bybitClient;
            _getAccountData = getAccountData;
        }

        public async Task<IEnumerable<SuperTrendResult>> getKlineAsync()
        {
            var timeStamp = await _client.RegisterClient().UsdPerpetualApi.ExchangeData.GetServerTimeAsync();
            TimeSpan date = timeStamp.Data.TimeOfDay;
            DateTime d = DateTime.UtcNow.AddHours(-16.6666);//addmiutes(200 * -5)
            var result = _client.RegisterClient().UsdPerpetualApi.ExchangeData.GetKlinesAsync(
                symbol,
                Bybit.Net.Enums.KlineInterval.FiveMinutes, 
                d);
            //////////////////////get indicator signals
            IEnumerable<BybitKline> kline = result.Result.Data;
            List<Quote> quotes = new List<Quote>();
            foreach (BybitKline k in kline)
            {
                Quote q = new Quote
                {
                    Close = k.ClosePrice,
                    Open = k.OpenPrice,
                    High = k.HighPrice,
                    Low = k.LowPrice,
                    Volume = k.Volume,
                    Date = k.OpenTime
                };
                quotes.Add(q);
            }
            IEnumerable<SuperTrendResult> results = quotes.GetSuperTrend(12, 3);
            int count = results.Count();
            var last = results.LastOrDefault<SuperTrendResult>();
            var last2 = results.ElementAtOrDefault<SuperTrendResult>(count - 2);

            //buy signal 
            if (last2.LowerBand == null && last.UpperBand == null)
            {
                decimal tp = kline.LastOrDefault<BybitKline>().ClosePrice + kline.LastOrDefault<BybitKline>().ClosePrice * ((kline.LastOrDefault<BybitKline>().ClosePrice - last.LowerBand.Value) / last.LowerBand.Value);
                await _placeOrder.placeOrderAsync(symbol,false, last.UpperBand.Value.WithDecimalDigitsOf(3), tp.WithDecimalDigitsOf(3), 50, false);
                Console.WriteLine("sell     stop loss = " + last.LowerBand.ToString()+ "take profit = "+ tp);

            }
            //sell signal
            else if (last2.UpperBand == null && last.LowerBand == null)
            {
                decimal tp = kline.LastOrDefault<BybitKline>().ClosePrice + kline.LastOrDefault<BybitKline>().ClosePrice * ((kline.LastOrDefault<BybitKline>().ClosePrice - last.UpperBand.Value) / last.UpperBand.Value);
                await _placeOrder.placeOrderAsync(symbol,false, last.UpperBand.Value.WithDecimalDigitsOf(3), tp.WithDecimalDigitsOf(3), 50, false);
                Console.WriteLine("buy      stop loss = " + last.UpperBand.ToString() + "take profit = " + tp);

            }
            else
                Console.WriteLine("do nothing");
            foreach (SuperTrendResult n in results)
            {
                File.WriteAllText("C:\\Users\\MAXELL\\Desktop\\output.json", n.SuperTrend.ToString());

            }
            return results;
        }

    }
}