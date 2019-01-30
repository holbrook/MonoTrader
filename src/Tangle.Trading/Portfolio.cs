using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tangle.Trading.Base;

namespace Tangle.Trading
{
    /// <summary>
    /// 投资组合
    /// </summary>
    public class Portfolio
    {
        /// <summary>
        /// 可用资金，为子账户可用资金的加总
        /// </summary>
        /// <value>The cash.</value>
        public decimal Cash { get; set; }

        /// <summary>
        /// 冻结资金，为子账户冻结资金加总
        /// </summary>
        /// <value>The frozen cash.</value>
        public decimal FrozenCash { get; set; }

        /// <summary>
        /// 市值。为子账户市值的加总
        /// </summary>
        /// <value>The market value.</value>
        public decimal MarketValue { get; set; }

        /// <summary>
        /// 所有的持仓。以代码为键
        /// </summary>
        /// <value>The positions.</value>
        //public Dictionary<string,Position> Positions { get; set; }

        //total_returns   decimal 投资组合至今的累积收益率
        //daily_returns decimal 投资组合每日收益率
        //daily_pnl decimal 当日盈亏，子账户当日盈亏的加总
        //total_value decimal 总权益，为子账户总权益加总
        //units   decimal 份额。在没有出入金的情况下，策略的初始资金
        //unit_net_value  decimal 单位净值
        //static_unit_net_value decimal 静态单位权益
        //transaction_cost decimal 当日费用
        //pnl decimal 当前投资组合的累计盈亏
        //start_date datetime.datetime   策略投资组合的回测/实时模拟交易的开始日期
        //annualized_returns  decimal 投资组合的年化收益率
    }
}
