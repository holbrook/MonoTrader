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
    public class Position : PositionBase
    {
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
