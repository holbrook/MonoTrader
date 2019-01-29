using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Tangle.Trading.Base;
using Tangle.Trading.Future;
using Tangle.Trading.Stock;

namespace Tangle.Trading
{
    [Export(typeof(IBroker))]
    [Broker(typeof(IStockBroker), new string[] { "XSHE", "XSHG", "NEEQ" })]
    [Broker(typeof(IFutureBroker), new string[] { "CCFX", "XDCE" , "XSGE", "XZCE" })]
    public class DemoBroker : IBroker, IStockBroker, IFutureBroker
    {
        public AccountBase Account => throw new NotImplementedException();

        public string AddOrder(string orderbookID, int quantity, ORDER_SIDE side, decimal price)
        {
            throw new NotImplementedException();
        }


        public void CancelOrder(Stock.Order order)
        {
            throw new NotImplementedException();
        }


        public List<Stock.Order> GetOpenOrders()
        {
            throw new NotImplementedException();
        }

        public void Initialize(dynamic param)
        {
            throw new NotImplementedException();
        }

        public Stock.Order OrderShares(string orderbookID, int amount)
        {
            return null;
        }

        public void SynchronizeAccount()
        {

        }

        Future.Order IFutureBroker.AddOrder(string orderbookID, int quantity, ORDER_SIDE side, POSITION_EFFECT effect, decimal price)
        {
            throw new NotImplementedException();
        }

        void IFutureBroker.CancelOrder(Future.Order order)
        {
            throw new NotImplementedException();
        }


        List<Future.Order> IFutureBroker.GetOpenOrders()
        {
            throw new NotImplementedException();
        }

        void IFutureBroker.SynchronizeAccount()
        {
            throw new NotImplementedException();
        }
    }
}
