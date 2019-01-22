using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoTrader.Market;
using Tangle.Trading.Base;

namespace Tangle.Trading
{
    /// <summary>
    /// 策略接口。这里只定义必须的接口
    /// 消息如<see cref="Tick"/>, <see cref="Match"/>等为可选接口，
    /// 通过实现<see cref="IStrategyHandle"/>来声明
    /// </summary>
    public interface IStrategy
    {
        void Initialize(Context ctx, dynamic config=null);
    }


    /// <summary>
    /// 标记接口
    /// </summary>
    public interface IStrategyEvent
    {

    }

    /// <summary>
    /// 标记接口
    /// </summary>
    public interface IStrategyHandle
    {
    }

    /// <summary>
    /// 定义策略需要接收哪些类型的消息。消息必须是<see cref="IStrategyEvent"/>类型。
    /// </summary>
    /// <typeparam name="TMessage"></typeparam>
    public interface IStrategyHandle<TMessage> : IStrategyHandle
        where TMessage : IStrategyEvent
    {
        void Handle(Context context, TMessage message);
    }
}
