using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTrader.Market
{
    public class Tick
    {
        /// <summary>
        /// 合约代码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 当前快照数据的时间戳
        /// </summary>
        public DateTime TimeStamp { get; set; }

        /// <summary>
        /// 当日开盘价
        /// </summary>
        public float Open { get; set; }

        /// <summary>
        /// 截止到当前的最高价
        /// </summary>
        public float High { get; set; }

        /// <summary>
        /// 截止到当前的最低价
        /// </summary>
        public float Low { get; set; }



//        last float 当前最新价
//prev_settlement float 昨日结算价
//prev_close float 昨日收盘价
//volume float 截止到当前的成交量
//limit_up float 涨停价
//limit_down float 跌停价
//open_interest float 截止到当前的持仓量
//asks list    卖出报盘价格，asks[0] 代表盘口卖一档报盘价
//ask_vols list    卖出报盘数量，ask_vols[0] 代表盘口卖一档报盘数量
//bids list    买入报盘价格，bids[0] 代表盘口买一档报盘价
//bids_vols list    买入报盘数量，bids_vols[0] 代表盘口买一档报盘数量

    }
}
