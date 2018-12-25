using System;
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
    }
}
