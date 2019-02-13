using System;
namespace TangleTrading.Future
{
    public class Tick
    {
        /// <summary>
        /// 证券代码
        /// </summary>
        public string OrderbookID { get; set; }

        /// <summary>
        /// 当前快照数据的时间戳
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// 当日开盘价
        /// </summary>
        public decimal Open { get; set; }

        /// <summary>
        /// 截止到当前的最高价
        /// </summary>
        public decimal High { get; set; }

        /// <summary>
        /// 截止到当前的最低价
        /// </summary>
        public decimal Low { get; set; }

        /// <summary>
        /// 最新价
        /// </summary>
        public decimal Last { get; set; }

        /// <summary>
        /// 昨日收盘价
        /// </summary>
        /// <value>The previous close.</value>
        public decimal PrevClose { get; set; }

        /// <summary>
        /// 截止到当前的成交量
        /// </summary>
        /// <value>The volume.</value>
        public int Volume { get; set; }

        /// <summary>
        /// 昨日结算价
        /// </summary>
        /// <value>The previous settlement price.</value>
        public decimal PrevSettlementPrice { get; set; }


        //limit_up decimal 涨停价
        //limit_down decimal 跌停价
        //open_interest decimal 截止到当前的持仓量


        /// <summary>
        /// 报卖价列表， AskPrices[0] 代表盘口卖一档报盘价
        /// </summary>
        /// <value>The ask prices.</value>
        public decimal[] AskPrices { get; set; }

        /// <summary>
        /// 报卖量列表， AskVolumes[0] 代表盘口卖一档报盘量
        /// </summary>
        /// <value>The ask volumes.</value>
        public int[] AskVolumes { get; set; }

        /// <summary>
        /// 报买价列表， AskPrices[0] 代表盘口买一档报盘价
        /// </summary>
        /// <value>The ask prices.</value>
        public decimal[] BidPrices { get; set; }

        /// <summary>
        /// 报买量列表， AskVolumes[0] 代表盘口买一档报盘量
        /// </summary>
        /// <value>The ask volumes.</value>
        public int[] BidVolumes { get; set; }

    }
}
