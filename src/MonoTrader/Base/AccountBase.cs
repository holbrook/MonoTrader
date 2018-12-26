using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTrader.Base
{
    /// <summary>
    /// 账户的基类
    /// </summary>
    public abstract class AccountBase
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

        

        //positions dict    一个包含股票子组合仓位的字典，以order_book_id作为键，position对象作为值，关于position的更多的信息可以在下面的部分找到。
        //dividend_receivable float 投资组合在分红现金收到账面之前的应收分红部分。具体细节在分红部分
    }
}
