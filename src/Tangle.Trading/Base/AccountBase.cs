using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tangle.Trading.Base
{
    public abstract class AccountBase
    { }
    /// <summary>
    /// 账户的基类
    /// </summary>
    public abstract class AccountBase<TPosition>
        where TPosition: PositionBase
    {
        /// <summary>
        /// 可用资金
        /// </summary>
        public float Cash { get; set; }

        /// <summary>
        /// 冻结资金
        /// </summary>
        public float FrozenCash { get; set; }
 
        /// <summary>
        /// 所有证券的市值
        /// </summary>
        public float MarketValue { get; set; }
        
        /// <summary>
        /// 总资产（证券市值+现金）
        /// </summary>
        public float TotalValue { get; set; }

        /// <summary>
        /// 当日交易费用
        /// </summary>
        public float TransactionCost { get; set; }

        /// <summary>
        /// 持仓信息
        /// </summary>
        public Dictionary<string,TPosition> Positions { get; set; }

        
    }
}
