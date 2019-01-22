using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tangle.Trading;

namespace MonoTrader.Market
{
    public class Tick: IStrategyEvent
    {
        /// <summary>
        /// 证券代码
        /// </summary>
        public string InstrumentID { get; set; }

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


//prev_settlement decimal 昨日结算价
//prev_close decimal 昨日收盘价
//volume decimal 截止到当前的成交量
//limit_up decimal 涨停价
//limit_down decimal 跌停价
//open_interest decimal 截止到当前的持仓量
//asks list    卖出报盘价格，asks[0] 代表盘口卖一档报盘价
//ask_vols list    卖出报盘数量，ask_vols[0] 代表盘口卖一档报盘数量
//bids list    买入报盘价格，bids[0] 代表盘口买一档报盘价
//bids_vols list    买入报盘数量，bids_vols[0] 代表盘口买一档报盘数量

    }
}
