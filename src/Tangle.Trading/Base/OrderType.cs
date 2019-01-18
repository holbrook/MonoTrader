using System;
namespace Tangle.Trading.Base
{

    public abstract class OrderType
    {

    }

    /// <summary>
    /// 市价单
    /// </summary>
    public class MarketOrder : OrderType
    {

    }

    /// <summary>
    /// 限价单
    /// </summary>
    public class LimitOrder:OrderType
    {
        public decimal LimitPrice { get; private set; }

        public LimitOrder(decimal limitPrice)
        {
            LimitPrice = limitPrice;
        }
    }
}
