using Tangle.Trading.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tangle.Trading.Stock
{
    public class Account:AccountBase<Position>
    {
        /// <summary>
        /// 未到账的应收分红
        /// </summary>
        public decimal DividendReceivable { get; set; }
    }
}
