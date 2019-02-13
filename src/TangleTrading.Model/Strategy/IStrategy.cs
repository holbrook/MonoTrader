using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoTrader.Market;
using TangleTrading.Adapter;
using TangleTrading.Base;

namespace TangleTrading.Strategy
{
    /// <summary>
    /// 策略接口。这里只定义必须的接口
    /// 消息如<see cref="Tick"/>, <see cref="Match"/>等为可选接口，
    /// 通过实现<see cref="IStrategyHandle"/>来声明
    /// </summary>
    public interface IStrategy
    {
        void Initialize(Context ctx, dynamic config=null);
        void Handle(Context ctx, FeedEventArgs feedEvent);
    }
}
