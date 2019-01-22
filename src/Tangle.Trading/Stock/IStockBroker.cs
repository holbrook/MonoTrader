using System;
using Tangle.Trading.Base;

namespace Tangle.Trading.Stock
{
    /// <summary>
    /// 股票交易网关要实现的接口。
    /// 策略扩展方法中也要实现该接口，以便策略直接调用
    /// </summary>
    public interface IStockBroker
    {
        Order OrderShares(string orderbookID, int amount, OrderType style = null);
    }
}
