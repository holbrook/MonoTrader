using System;
using TangleTrading.Model.Base;

namespace TangleTrading.Model
{
    public static class StrategyFutureBrokerExtensions
    {
        /// <summary>
        /// 买开（期货专用）
        /// </summary>
        /// <returns>The open.</returns>
        /// <param name="stgy">Stgy.</param>
        /// <param name="instrumentID">Instrument identifier.</param>
        /// <param name="amount">Amount.</param>
        /// <param name="price">Price.</param>
        /// <param name="style">Style.</param>
        public static TangleTrading.Model.Future.Order BuyOpen(this IStrategy stgy,
        string instrumentID, int amount, decimal price=-1)
        {
            return null;
        }
    }
}
