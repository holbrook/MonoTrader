using System;
namespace TangleTrading.Model.Base
{
    /// <summary>
    /// 成交信息
    /// </summary>
    public abstract class Match
    {
        #region 基本信息
        /// <summary>
        /// 交易市场
        /// </summary>
        /// <value>The exchange identifier.</value>
        public string ExchangeID { get; set; }

        /// <summary>
        /// 证券代码
        /// </summary>
        public string OrderbookID { get; set; }

        /// <summary>
        /// 证券名称(简称）
        /// </summary>
        public string InstrumentName { get; set; }
        #endregion


        #region 成交信息
        /// <summary>
        /// 成交编号
        /// </summary>
        /// <value>The matching identifier.</value>
        public string MatchingID { get; set; }

        /// <summary>
        /// 成交时间
        /// </summary>
        /// <value>The matched time.</value>
        public DateTime MatchedTime { get; set; }

        /// <summary>
        /// 成交价格。对于撤单成交返回0
        /// </summary>
        /// <value>The match price.</value>
        public decimal MatchedPrice { get; set; }

        /// <summary>
        /// 成交金额
        /// </summary>
        /// <value>The match amount.</value>
        public decimal MatchedAmount { get; set; }

        /// <summary>
        /// 成交数量
        /// </summary>
        /// <value>The match volume.</value>
        public int MatchedQuantity { get; set; }

        /// <summary>
        /// 撤单数量
        /// </summary>
        /// <value>The cancelled quantity.</value>
        public int CancelledQuantity { get; set; }


        #endregion

        #region 委托信息

        /// <summary>
        /// 委托时间
        /// </summary>
        /// <value>The order time.</value>
        public DateTime OrderTime { get; set; }

        /// <summary>
        /// 合同号/订单号
        /// </summary>
        /// <value>The order identifier.</value>
        public string OrderID { get; set; }

        /// <summary>
        /// 委托数量  
        /// </summary>
        /// <value>The order quantity.</value>
        public int OrderQuantity { get; set; }

        /// <summary>
        /// 委托价格
        /// </summary>
        /// <value>The order price.</value>
        public decimal OrderPrice { get; set; }

        #endregion

        #region 未处理
        /* serialnum   流水号     
           acctId  资金帐号        
            regId   股东代码        

            reckoningamt    资金发生数       
            fullknockamt    全价成交金额      
            accuredinterestamt  应计利息金额      
            accuredinterest 应计利息        
            returnflag  回报类型        " 0：委托确认成功 1：委托确认非法 2：成交  3：交易所撤单成功 4：根网系统内部撤单成功 
            5：交易所撤单非法"
            occurTime   发生时间        
            batchnum    委托批号        
            forderserialnum 委托记录的序（流水）号    
            ordersource 订单来源        
            ordersourceid   委托标识ID      
            coveredflag 备兑标签(0-备兑,1-非备兑)  
            appserverip APPSVRID  
         */
        #endregion

        /*
closepnl    平仓盈亏        
openusedmarginamt   今开占用保证金     
offsetmarginamt 平仓释放保证金     
orderType   买卖方向        
premium 期权权利金收支     
commision   手续费     

actionflag  报单操作类型      
tradefrozenamt  交易冻结        
totalknockqty   总成交数量       
totalknockamt   总成交金额       

actiontime  修改时间        
exchErrorCode   交易所错误编码     
*/
    }
}
