using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tangle.Trading.Base;

namespace Tangle.Trading
{
    /// <summary>
    /// 策略上下文
    /// </summary>
    public class Context
    {
        public Portfolio Portfolio { get; private set; }

        // Account 应该放到一个Pool中，随时扩充
        public Dictionary<string, AccountBase> Accounts { get; private set; }

        //取股票账号。 这个接口是为了兼容 RiceQuant
        public Tangle.Trading.Stock.Account StockAccount { get
            {
                return null;
             }
        }

        //取期货账号。 这个接口是为了兼容 RiceQuant
        public Tangle.Trading.Future.Account FutureAccount
        {
            get
            {
                return null;
            }
        }
    }
}
