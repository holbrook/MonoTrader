﻿using System;
using System.Collections.Generic;
using Tangle.Trading.Base;

namespace Tangle.Trading.Future
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
    }
}
