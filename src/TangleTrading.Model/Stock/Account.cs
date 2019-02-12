using TangleTrading.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TangleTrading.Model.Stock
{
    public class Account
    {
        /// <summary>
        /// 可用资金
        /// </summary>
        public decimal Cash { get; set; }

        /// <summary>
        /// 冻结资金
        /// </summary>
        public decimal FrozenCash { get; set; }

        /// <summary>
        /// 所有证券的市值
        /// </summary>
        public decimal MarketValue { get; set; }

        /// <summary>
        /// 总资产（证券市值+现金）
        /// </summary>
        public decimal TotalValue { get; set; }

        /// <summary>
        /// 当日交易费用
        /// </summary>
        public decimal TransactionCost { get; set; }

        /// <summary>
        /// 持仓信息
        /// </summary>
        public Dictionary<string, Position> Positions { get; set; }

        /// <summary>
        /// 未到账的应收分红
        /// </summary>
        public decimal DividendReceivable { get; set; }

        public Account()
        {
            Positions = new Dictionary<string, Position>();
        }
    }
}
