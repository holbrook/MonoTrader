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
        SELL =2
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


//datetime    datetime.datetime 订单创建时间
//side SIDE    订单方向
//price   decimal 订单价格，只有在订单类型为'限价单'的时候才有意义
//quantity    int 订单数量
//filled_quantity int 订单已成交数量
//unfilled_quantity int 订单未成交数量
//type ORDER_TYPE  订单类型
//transaction_cost    decimal 费用
//avg_price decimal 成交均价
//status ORDER_STATUS    订单状态
//message str 信息。比如拒单时候此处会提示拒单原因
//trading_datetime    datetime.datetime 订单的交易日期（对应期货夜盘）
//position_effect POSITION_EFFECT 订单开平（期货专用）
    }
}
