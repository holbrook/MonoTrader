using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTrader.Base
{
    public abstract class PositionBase
    {
        //        order_book_id str 合约代码
        //quantity    int 当前持仓股数
        //pnl float 持仓累计盈亏
        //sellable int 该仓位可卖出股数。T＋1的市场中sellable = 所有持仓-今日买入的仓位
        //market_value    float 获得该持仓的实时市场价值
        //value_percent float 获得该持仓的实时市场价值在总投资组合价值中所占比例，取值范围[0, 1]
        //avg_price   float 平均建仓成本
    }
}
