using System;
using System.Collections.Generic;
using Tangle.Trading.Base;

namespace Tangle.Trading.Stock
{
    /// <summary>
    /// 股票交易网关要实现的接口。
    /// 策略扩展方法中也要实现该接口，以便策略直接调用
    /// </summary>
    public interface IStockBroker
    {
        /// <summary>
        /// 下单。
        /// </summary>
        /// <returns>The shares.</returns>
        /// <param name="orderbookID">Orderbook identifier.</param>
        /// <param name="quantity">Amount.</param>
        /// <param name="price">限价。不送表示市价单</param>
        Order AddOrder(string orderbookID, int quantity, ORDER_SIDE side, decimal price);
        void CancelOrder(Order order);
        List<Order> GetOpenOrders();

        /// <summary>
        /// 同步资金和持仓
        /// </summary>
        void SynchronizeAccount();
    }
}
