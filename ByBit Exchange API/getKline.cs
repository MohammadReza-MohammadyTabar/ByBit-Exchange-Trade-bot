using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Bybit.Net.Objects.Models;
using ByBit_Exchange_API.Interfaces;
using Skender.Stock.Indicators;
using System.Linq;
using System.IO;
using ByBit_Exchange_API.Controllers;
using EmailService;

namespace ByBit_Exchange_API
{
    public class getKline : IGetKline
    {
        private static IByBitClient _client;
        private static IPlaceOrder _placeOrder;
        private static PlaceOrderController _placeOrderController;
        private static IGetAccountData _getAccountData;
        private static IClosePositions _closePositions;
        private string toEmailAddress = Settings.items.toEmailAddress;
        private decimal lavrage = Settings.items.lagvrage;

        public getKline(IByBitClient bybitClient,IPlaceOrder placeOrder, PlaceOrderController placeOrderController,IGetAccountData getAccountData,IClosePositions closePositions)
        {
            _placeOrderController = placeOrderController;
            _placeOrder = placeOrder;
            _client = bybitClient;
            _getAccountData = getAccountData;
            _closePositions = closePositions;
        }

        public async Task<IEnumerable<SuperTrendResult>> getKlineAsync(string symbol, decimal quantityPercent)
        {
            //var timeStamp = await _client.RegisterClient().UsdPerpetualApi.ExchangeData.GetServerTimeAsync();
            //TimeSpan date = timeStamp.Data.TimeOfDay;
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
            IEnumerable<SuperTrendResult> results = quotes.GetSuperTrend(Settings.items.STlookbackPeriods, Settings.items.STperiod);
            int count = results.Count();
            var last = results.ElementAtOrDefault<SuperTrendResult>(count - 2);
            var last2 = results.ElementAtOrDefault<SuperTrendResult>(count - 3);
            //buy signal 
            if (last2.LowerBand == null && last.UpperBand == null)
            {
                if (await _closePositions.closePosition(symbol))
                {

                    decimal tp = kline.ElementAtOrDefault<BybitKline>(count -2).ClosePrice + kline.ElementAtOrDefault<BybitKline>(count - 2).ClosePrice * ((kline.ElementAtOrDefault<BybitKline>(count - 2).ClosePrice - last.LowerBand.Value) / last.LowerBand.Value);
                    decimal balance = _getAccountData.getBalanceAsync().Result.Data.Values.FirstOrDefault<BybitBalance>(m => m.AvailableBalance != 0).AvailableBalance;
                    decimal qtty = balance * quantityPercent / kline.LastOrDefault<BybitKline>().ClosePrice*lavrage;
                    var place=await _placeOrder.placeOrderAsync(symbol, true, last.LowerBand.Value.WithDecimalDigitsOf(3), tp.WithDecimalDigitsOf(3), quantity: qtty.WithDecimalDigitsOf(0), false);
                    SendEmail email = new SendEmail(toEmailAddress, "buy position opend", DateTime.Now.ToString() + "\n buy \n stop loss =" + last.LowerBand.ToString() + "\ntake profit = " + tp+"\nquantity = "+qtty.WithDecimalDigitsOf(0)+"\nOrder Price = "+place.Data.Price+"\nBlance = "+balance);
                    Console.WriteLine("buy     stop loss = " + last.LowerBand.ToString() + "take profit = " + tp);
                }

            }
            //sell signal
            else if (last2.UpperBand == null && last.LowerBand == null)
            {
                if (await _closePositions.closePosition(symbol))
                {
                    decimal tp = kline.ElementAtOrDefault<BybitKline>(count - 2).ClosePrice + kline.ElementAtOrDefault<BybitKline>(count - 2).ClosePrice * ((kline.ElementAtOrDefault<BybitKline>(count - 2).ClosePrice - last.UpperBand.Value) / last.UpperBand.Value);
                    decimal balance = _getAccountData.getBalanceAsync().Result.Data.Values.FirstOrDefault<BybitBalance>(m => m.AvailableBalance != 0).AvailableBalance;
                    decimal qtty = balance * quantityPercent / kline.LastOrDefault<BybitKline>().ClosePrice*lavrage;
                    var place=await _placeOrder.placeOrderAsync(symbol, false, last.UpperBand.Value.WithDecimalDigitsOf(3), tp.WithDecimalDigitsOf(3), quantity:qtty.WithDecimalDigitsOf(0), false);
                    SendEmail email = new SendEmail(toEmailAddress, "sell position opend", DateTime.Now.ToString() + "\n sell \n stop loss =" + last.UpperBand.ToString() + "\ntake profit = " + tp + "\nquantity = " + qtty.WithDecimalDigitsOf(0) + "\nOrder Price = " + place.Data.Price + "\nBlance = " +balance);
                    Console.WriteLine("sell      stop loss = " + last.UpperBand.ToString() + "take profit = " + tp);
                }
            }
            else
                Console.WriteLine("do nothing");

            //string s = "";
            //foreach (SuperTrendResult n in results)
            //{
            //    s += n.Date.ToString() + "       "
            //        + "lowerband    " + n.LowerBand.ToString() +"       "
            //        + "uperband     " + n.UpperBand.ToString() + "       "
            //        + "\n";
            //}
            //File.WriteAllText("\\output.json", s);

            return results;
        }

    }
}