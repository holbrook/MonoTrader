using System;
namespace TangleTrading.Model
{
    /// <summary>
    /// 策略行情类的扩展方法，在编写策略时可以直接调用
    /// </summary>
    public static class StrategyMarketDataExtensions
    {
        /// <summary>
        /// 订阅合约行情。该操作会导致合约池内合约的增加
        /// </summary>
        /// <param name="stgy">Stgy.</param>
        /// <param name="orderbookID">证券代码</param>
        //public static void Subscribe(this IStrategy stgy, string orderbookID)
        //{

        //}

        /// <summary>
        /// 取消订阅合约行情。取消订阅会导致合约池内合约的减少，如果当前合约池中没有任何合约，则策略直接退出。
        /// </summary>
        /// <param name="stgy">Stgy.</param>
        /// <param name="orderbookID">Instrument identifier.</param>
        //public static void UnSubscribe(this IStrategy stgy, string orderbookID)
        //{

        //}
    }
}
