using MonoTrader.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTrader.Future
{
    public class Account:AccountBase<BiPosition>
    {
        /// <summary>
        /// 已占用保证金
        /// </summary>
        public float Margin { get; set; }

        /// <summary>
        /// 多头保证金
        /// </summary>
        public float BuyMargin { get; set; }

        /// <summary>
        /// 空头保证金
        /// </summary>
        public float SellMargin { get; set; }

        /// <summary>
        /// 当日盈亏。
        /// = HoldingProfit(当日浮动盈亏) + RealizedProfit(当日平仓盈亏) - TransactionCost(当日费用)
        /// </summary>
        public float DailyProfit { get; set; }

        /// <summary>
        /// 当日浮动盈亏
        /// </summary>
        public float HoldingProfit { get; set; }

        /// <summary>
        /// 当日平仓盈亏
        /// </summary>
        public float RealizedProfit { get; set; }
    }
}
