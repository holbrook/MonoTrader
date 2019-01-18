using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoTrader.Market;

namespace Tangle.Trading
{
    /// <summary>
    /// 策略接口
    /// </summary>
    public interface IStrategy
    {
        void Initialize(dynamic config);
        void HandleTick(Tick tick);
        void HandleMatch(Match match);
    }
}
