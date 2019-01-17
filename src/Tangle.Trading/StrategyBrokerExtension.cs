using System;
using Tangle.Trading.Base;
using Tangle.Trading.Stock;

namespace Tangle.Trading
{
    /// <summary>
    /// 策略交易类的扩展方法，在编写策略时可以直接调用
    /// </summary>

    /*
    // 先做最急需的。
    // 对交易来说, 主动调用的函数有用的只有下单, 撤单, 查询持仓, 查询未成交订单几个. 
    // 而被动的函数(callback)都不是必须的

    // 1. 下单(add_order), 撤单(del_order), 查询未成交订单(query_order), 同步仓位(sync_position), 同步账户资金(sync_account). 这几个是交易中最常用, 最必须的几个.
2. C++ bind 到python只处理2里面的几个接口与相关数据结构, 而不需要处理全部的函数和结构.
3. 行情和交易分开
4. 每一个account对应一个tradeapi, 而且account应该设计为一个pool.
*/
    public static class StrategyBrokerExtension
    {

        /// <summary>
        /// 指定股数交易（股票专用）, 对应 RiceQuant 的 order_shares
        /// </summary>
        /// <returns>The shares.</returns>
        /// <param name="stgy">Stgy.</param>
        /// <param name="instrumentID">Identifier or ins.</param>
        /// <param name="amount">需要落单的股数。正数代表买入，负数代表卖出。将会根据一手xx股来向下调整到一手的倍数，比如中国A股就是调整成100股的倍数。</param>
        /// <param name="style">订单类型，默认是市价单。目前支持的订单类型有：
        /// <list type="">
        /// <item>style=MarketOrder()</item>
        /// <item>style=LimitOrder(limit_price)</item>
        /// </list>
        ///</param>
        public static Order OrderShares(this IStrategy stgy, string instrumentID, int amount, OrderType style= null)
        {
            //TODO: if null, default as MarketOrder
            Console.WriteLine(string.Format("调用{0},{1}", instrumentID, amount));
            return null;
        }


    }
}
