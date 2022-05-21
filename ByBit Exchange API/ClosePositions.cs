using Bybit.Net.Objects.Models;
using ByBit_Exchange_API.Interfaces;
using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ByBit_Exchange_API
{

    public class ClosePositions: IClosePositions
    {
        private static IGetAccountData _getAccountData;
        private static IPlaceOrder _placeOrder;
        private static ICancelOrder _cancelOrder;
        public ClosePositions(IGetAccountData getAccountData, IPlaceOrder placeOrder,ICancelOrder cancelOrder)
        {
            _cancelOrder = cancelOrder;
            _placeOrder = placeOrder;
            _getAccountData = getAccountData;
        }

        public async Task<bool> closePosition(string symbol)
        {
            var postions = await _getAccountData.GetPositionAsync(symbol);
            //cancelling buy position and orders

            if (postions.Data.ElementAtOrDefault(0).Quantity != 0)
            {
                var resault = await _placeOrder.placeOrderAsync(symbol, false, 0, 0, postions.Data.ElementAtOrDefault(0).Quantity, true);
                var resault2 = await _cancelOrder.CancelAllOrders(symbol);
                return resault.Success && resault2.Success;
            }
            //cancelling sell positon and orders
            else if (postions.Data.ElementAtOrDefault(1).Quantity != 0)
            {
                var resault = await _placeOrder.placeOrderAsync(symbol, false, 0, 0, postions.Data.ElementAtOrDefault(1).Quantity, true);
                var resault2 = await _cancelOrder.CancelAllOrders(symbol);
                return resault.Success && resault2.Success;
            }
            else
                return true;
            
        }

        
    }
}
