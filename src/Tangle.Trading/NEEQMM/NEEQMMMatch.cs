using System;
namespace Tangle.Trading.NEEQMM
{
    public class NEEQMMMatch
    {
        public string MatchID { get; private set; }                // char szMatchedSN[16+1];          ///< 成交编号
        public DateTime MatchTime { get; private set; }        //成交时间
        int MatchType { get; set; }                            //成交类型      nStkBiz;            
        public string TypeName
        {
            get
            {
                if (55000003 == MatchType)
                    return "做市买入";
                if (55000004 == MatchType)
                    return "做市卖出";
                return "未知";
            }
        }
        bool IsCanceled { get; set; }                          // 是否撤单      chIsWithdraw[1+1];         ///< 撤单标志

        public double MatchedPrice { get; private set; }       //成交价格
        public int MatchedVolume { get; private set; }   //成交数量
        public double MatchedAmount { get; private set; }         ///< 成交金额
                                                                  ///
        public string OrderID { get; private set; }            // 委托合同序号
        public double OrderPrice { get; private set; }        //委托价格
        public long OrderVolume { get; private set; }       //委托数量   
    }
}
