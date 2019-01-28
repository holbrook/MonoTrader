using System;
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

        public Stock.Order OrderShares(string orderbookID, int amount, OrderType style = null)
        {
            return null;
        }

        public void SynchronizeAccount()
        {

        }
    }
}
