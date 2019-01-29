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

        Order AddOrder(string orderbookID, int quantity, ORDER_SIDE side, POSITION_EFFECT effect, decimal price = -1);
        void CancelOrder(Order order);

        List<Order> GetOpenOrders();

        /// <summary>
        /// 同步资金和持仓
        /// </summary>
        void SynchronizeAccount();
    }
}
