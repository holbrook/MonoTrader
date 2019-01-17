using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tangle.Trading
{
    /// <summary>
    /// 策略上下文
    /// </summary>
    public class Context
    {
        public Portfolio Portfolio { get; set; }
        public Tangle.Trading.Stock.Account StockAccount { get; set; }
    }
}
