using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tangle.Trading.Base
{
    public abstract class PositionBase
    {
        

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



    }
}
