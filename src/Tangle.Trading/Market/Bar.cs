using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTrader.Market
{
    public class Bar
    {
        /// <summary>
        /// 证券代码
        /// </summary>
        public string InstrumentID { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public DateTime Timestamp { get; set; }



//symbol  str 合约简称

//open    decimal 开盘价
//close   decimal 收盘价
//high    decimal 最高价
//low decimal 最低价
//volume  decimal 成交量
//total_turnover  decimal 成交额
//prev_close  decimal 昨日收盘价
//limit_up    decimal 涨停价
//limit_down  decimal 跌停价
//isnan   bool 当前bar数据是否有行情。例如，获取已经到期的合约数据，isnan此时为True
//suspended   bool 是否全天停牌
//prev_settlement decimal 昨结算（期货日线数据专用）
//settlement  decimal 结算（期货日线数据专用）
    }
}
