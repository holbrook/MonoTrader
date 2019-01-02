using MonoTrader.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTrader.Future
{
    /// <summary>
    /// 期货双向持仓汇总
    /// 其中，对于空方向持仓，MarketValue为负值
    /// </summary>
    public class BiPosition : PositionBase
    {

        //holding_pnl float 当日持仓盈亏
        //realized_pnl float 当日平仓盈亏

        /// <summary>
        /// 当日盈亏，当日浮动盈亏+当日平仓盈亏
        /// </summary>
        public float TodayProfit { get; set; }


        public float TodayHoldingProfit { get; set; }


        /// <summary>
        /// 仓位交易费用
        /// </summary>
        public float TransactionCost { get; set; }

        /// <summary>
        /// 仓位总保证金
        /// </summary>
        public float Margin { get; set; }

        public Position LongPosition { get; set; }
        public Position ShortPosition { get; set; }
    }
}
