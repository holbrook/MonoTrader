using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tangle.Trading.Base
{

    public enum ORDER_STATUS
    {
        NEW = 0,                         //未报
        PENDING = 1,                             //正报，待报
        ACTIVE = 2,                           //已报，可撤
        FILLED = 3,                           //全成
        PENDING_CANCEL = 4,                              //待撤：已报待撤，部成待撤
        CANCELLED = 5,                                      //已撤
        PARTIALLY_CANCELLED = 6,                                  //部撤，部成已撤                                
        REJECTED = 7                                               //废单，拒单
    }

    public enum ORDER_SIDE
    {
        /// <summary>
        /// 买
        /// </summary>
        BUY =1 ,

        /// <summary>
        /// 卖
        /// </summary>
        SELL =2,

        UNKNOWN = 0
    }

    public enum ORDER_TYPE
    {
        /// <summary>
        /// 市价单
        /// </summary>
        MARKET = 1,

        /// <summary>
        /// 限价单
        /// </summary>
        LIMIT =2
    }




    public abstract class OrderBase
    {
        /// <summary>
        /// 唯一标识订单的id
        /// </summary>
        public string OrderID { get; set; }

        /// <summary>
        /// 合约代码
        /// </summary>
        public string OrderbookID { get; set; }

        /// <summary>
        /// 合约名称
        /// </summary>
        /// <value>The name of the orderbook.</value>
        public string OrderbookName { get; set; }

        /// <summary>
        /// 订单方向
        /// </summary>
        /// <value>The side.</value>
        public ORDER_SIDE Side { get; set; }

        /// <summary>
        /// 委托价格，只有在订单类型为'限价单'的时候才有意义
        /// </summary>
        /// <value>The order price.</value>
        public decimal OrderPrice { get; set; }

        /// <summary>
        /// 委托数量
        /// </summary>
        /// <value>The quantity.</value>
        public int Quantity { get; set; }

        /// <summary>
        /// 成交数量
        /// </summary>
        /// <value>The filled quantity.</value>
        public int FilledQuantity { get; set; }

        /// <summary>
        /// 撤单数量
        /// </summary>
        /// <value>The un filled quantity.</value>
        public int  CancelledQuantity { get; set; }

        /// <summary>
        /// 订单创建时间
        /// </summary>
        /// <value>The order time.</value>
        public DateTime OrderTime { get; set; }

        /// <summary>
        /// 成交均价
        /// </summary>
        /// <value>avg_price</value>
        public decimal FilledPrice { get; set; }

        //type ORDER_TYPE  订单类型
        //transaction_cost    decimal 费用

        //status ORDER_STATUS    订单状态
        //message str 信息。比如拒单时候此处会提示拒单原因

    }
}
