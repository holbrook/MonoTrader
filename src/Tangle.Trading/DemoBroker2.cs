using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Tangle.Trading.Base;
using Tangle.Trading.NEEQMM;
using Tangle.Trading.Stock;

namespace Tangle.Trading
{
    [Export(typeof(IBroker))]
    [Broker(typeof(INEEQMMBroker), new string[] { "NEEQ" })]

    public class DemoBroker2 : IBroker, IStockBroker
    {
        public DemoBroker2()
        {
        }

        public AccountBase Account => throw new NotImplementedException();

        public void Initialize(dynamic param)
        {
            throw new NotImplementedException();
        }



        public void SynchronizeAccount()
        {
            throw new NotImplementedException();
        }

        public string AddOrder(string orderbookID, int quantity, ORDER_SIDE side, decimal price)
        {
            throw new NotImplementedException();
        }

        void IStockBroker.CancelOrder(Order order)
        {
            throw new NotImplementedException();
        }

        List<Order> IStockBroker.GetOpenOrders()
        {
            throw new NotImplementedException();
        }

        void IStockBroker.SynchronizeAccount()
        {
            throw new NotImplementedException();
        }
    }
}
