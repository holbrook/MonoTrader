using System;
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

        public Order OrderShares(string orderbookID, int amount, OrderType style = null)
        {
            throw new NotImplementedException();
        }

        public void SynchronizeAccount()
        {
            throw new NotImplementedException();
        }
    }
}
