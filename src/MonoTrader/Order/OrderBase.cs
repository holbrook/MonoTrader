﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTrader.Order
{

    public enum OrderStatus
    {
        /*
         * PENDING_NEW	待报
ACTIVE	可撤
FILLED	全成
PENDING_CANCEL	待撤
CANCELLED	已撤
REJECTED	拒单
*/
    }

    public enum OrderSide
    {
        /*
         * BUY	买
SELL	卖
*/
    }

    public enum POSITION_EFFECT - 开平
枚举值 说明
OPEN    开仓
CLOSE   平仓

        public enum ORDER_TYPE - 订单类型
枚举值 说明
MARKET  市价单
LIMIT   限价单

    public abstract class OrderBase
    {
//        order_id int 唯一标识订单的id
//order_book_id str 合约代码
//datetime    datetime.datetime 订单创建时间
//side SIDE    订单方向
//price   float 订单价格，只有在订单类型为'限价单'的时候才有意义
//quantity    int 订单数量
//filled_quantity int 订单已成交数量
//unfilled_quantity int 订单未成交数量
//type ORDER_TYPE  订单类型
//transaction_cost    float 费用
//avg_price float 成交均价
//status ORDER_STATUS    订单状态
//message str 信息。比如拒单时候此处会提示拒单原因
//trading_datetime    datetime.datetime 订单的交易日期（对应期货夜盘）
//position_effect POSITION_EFFECT 订单开平（期货专用）
    }
}
