using Tangle.Trading.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tangle.Trading.Future
{
    /// <summary>
    /// 期货单向持仓数据（多头或空头）
    /// </summary>
    public class Position
    {
        public string OrderbookID { get; set; }
        public string OrderbookName { get; set; }

        /// <summary>
        /// 持仓累计盈亏
        /// </summary>
        public decimal Profit { get; set; }

        /// <summary>
        /// 持仓市值
        /// </summary>
        public decimal MarketValue { get; set; }

        /// <summary>
        /// 平均成本
        /// </summary>
        public decimal AvgPrice { get; set; }

        /// <summary>
        /// 当前持仓
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// 今仓。对于T+1股票，可用=持仓-今仓
        /// </summary>
        public int TodayQuantity { get; set; }


        /// <summary>
        /// 当日盈亏
        /// </summary>
        public decimal DailyProfit {get;set;}

        /// <summary>
        /// 交易费用
        /// </summary>
        public decimal TransactionCost { get; set; }

        /// <summary>
        /// 可平持仓
        /// </summary>
        public int ClosableQuantity { get; set; }

        /// <summary>
        /// 占用保证金
        /// </summary>
        public decimal Margin { get; set; }

        /// <summary>
        /// 开仓均价
        /// </summary>
        public decimal AvgOpenPrice { get; set; }

        
    }
}
