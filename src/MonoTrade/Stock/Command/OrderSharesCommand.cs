using System;
using MonoTrader.Base;

namespace MonoTrade.Stock.Command
{
    /// <summary>
    /// 指定股数交易（股票专用）
    /// </summary>
    public class OrderSharesCommand
    {
        public string InstrumentID { get; private set; }


        /// <summary>
        /// 落指定股数的买/卖单，最常见的落单方式之一。可以下市价单或者限价单。
        /// 如果是市价单，可以不传入价格
        /// </summary>
        /// <param name="instrumentID">证券代码</param>
        /// <param name="side">委托方向</param>
        /// <param name="quantity">委托数量</param>
        /// <param name="type">委托方式</param>
        /// <param name="price">委托价格</param>
        public OrderSharesCommand(string instrumentID, OrderSide side, int quantity, OrderType type, float price=0)
        {
            InstrumentID = instrumentID;
        }
    }
}
