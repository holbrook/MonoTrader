using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tangle.Trading.Base;

namespace Tangle.Trading.Future
{
    public interface IFutureBroker
    {
        Account FutureAccountInfo { get; }

        string AddFutureOrder(string orderbookID, int quantity, ORDER_SIDE side, POSITION_EFFECT effect, decimal price = -1);
        void CancelFutureOrder(string orderID);

        List<Order> GetOpenFutureOrders();

        /// <summary>
        /// 同步资金和持仓
        /// </summary>
        void SynchronizeFutureAccount();
    }
}
