using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTrader.Instrument
{
    /// <summary>
    /// 合约状态
    /// </summary>
    public enum InstrumentStatus
    {
        /// <summary>
        /// 正常
        /// </summary>
        Active = 0,

        /// <summary>
        /// 停牌
        /// </summary>
        Suspended = 1,

        /// <summary>
        /// 退市
        /// </summary>
        Delisted = 2 
        
    }
    /**
     * 证券的基类
     */
    

    public abstract class InstrumentBase
    {
        /// <summary>
        /// 证券代码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 证券简称
        /// </summary>
        public string Symbol { get; set; }

        /// <summary>
        /// 一手的数量
        /// </summary>
        public int RoundLot { get; set; }

        /// <summary>
        /// 最小价格变动单位
        /// </summary>
        public float TickSize { get; set; }

        /// <summary>
        /// 上市日期
        /// </summary>
        public DateTime ListedDate { get; set; }

        /// <summary>
        /// 合约已上市天数。
        /// 如果合约首次上市交易，天数为0；如果合约尚未上市或已经退市，则天数值为-1
        /// </summary>
        public int DaysFromList
        {
            get
            {
                if (null == ListedDate)
                    return -1;
                return (DateTime.Now - ListedDate).Days;
            }
        }

        /// <summary>
        /// 退市日期
        /// </summary>
        public DateTime DeListedDate { get; set; }

        /// <summary>
        /// 交易所，如：
        /// <list type="bullet">
        /// <item><description>'XSHG' - 上交所</description></item>
        /// <item><description>'XSHE' - 深交所</description></item>
        /// <item><description>'NEEQ' - 股转公司</description></item>
        /// <item><description>'DCE' - 大连商品交易所</description></item>
        /// <item><description>'SHFE' - 上海期货交易所</description></item>
        /// <item><description>'CFFEX' - 中国金融期货交易所</description></item>
        /// <item><description>'CZCE'- 郑州商品交易所</description></item>
        /// </list>
        /// </summary>
        public string Exchange { get; set; }

        /// <summary>
        /// 板块类别，如:
        /// <list type="bullet">
        /// <item><description>'MAIN' - 主板</description></item>
        /// <item><description>'GEM' - 创业板</description></item>
        /// <item><description>'SME' - 中小板</description></item>
        /// </list>
        /// </summary>
        public string Board { get; set; }
        
        public InstrumentStatus Status { get; set; }

        public string StatusName { get
            {
                switch(Status)
                {
                    case InstrumentStatus.Active:
                        return "正常";
                    case InstrumentStatus.Suspended:
                        return "停牌";
                    case InstrumentStatus.Delisted:
                        return "退市";
                }
                return "未知";
            }
        }
    }
}
