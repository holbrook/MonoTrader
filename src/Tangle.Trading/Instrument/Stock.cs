using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTrader.Instrument
{
    /// <summary>
    /// 股票
    /// </summary>
    public class Stock : InstrumentBase
    {
        /// <summary>
        /// 板块代码
        /// </summary>
        //public string SectorCode { get; set; }

        /// <summary>
        /// 板块名称
        /// </summary>
        //public string SectorName { get; set; }

        /// <summary>
        /// 行业代码
        /// </summary>
        public string IndustryCode {get;set;}

        /// <summary>
        /// 行业名称
        /// </summary>
        public string IndustryName { get; set; }

        /// <summary>
        /// 概念
        /// </summary>
        public List<string> ConceptNames { get; set; }

        /// <summary>
        /// 特殊状态，如：
        /// <list type="bullet">
        /// <item><description>'ST' - ST处理</description></item>
        /// <item><description>'StarST' - *ST代表该股票正在接受退市警告</description></item>
        /// <item><description>'PT' - 代表该股票连续3年收入为负，将被暂停交易</description></item>
        /// <item><description>'Other' - 其他</description></item>
        /// </list>
        /// </summary>
        public string SpecialLabel { get; set; }
    }
}
