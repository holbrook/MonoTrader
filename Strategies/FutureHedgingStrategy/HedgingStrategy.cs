using System;
using MonoTrader.Market;
using Tangle.Trading;
using Tangle.Trading.Base;

namespace HedgingStrategy
{
    /// <summary>
    /// 对冲策略。跟踪一个标的的持仓，当持仓变化达到一定数量时，进行对冲操作
    /// </summary>
    public class HedgingStrategy : IStrategy, IStrategyHandle<Tick>
    {
        public void Initialize(Context ctx, dynamic config=null)
        {
            //TODO: 从 config 中读取并设置
            ctx.Securities = new string[] { "512520.XSHE", "IH1902.CCFX" };
            ctx.CustomData.Threshold = 1000000;
            if(ctx.Portfolio.Positions.ContainsKey(ctx.Securities[0]))
            {
                ctx.CustomData.PositionBase = ctx.Portfolio.Positions[ctx.Securities[0]].Quantity;
            }
            else
            {
                ctx.CustomData.PositionBase = 0;
            }

        }

        public void Handle(Context ctx, Tick msg)
        {
            if (msg.InstrumentID != ctx.Securities[0])
                return;

            //读取持仓. TODO: 定期更新持仓
            int lastPosition = ctx.CustomData.PositionBase;
            if (ctx.Portfolio.Positions.ContainsKey(ctx.Securities[0]))
            {
                lastPosition = ctx.Portfolio.Positions[ctx.Securities[0]].Quantity;
            }

            var delta = lastPosition - ctx.CustomData.PositionBase;


            this.Shout();
        }
    }
}
