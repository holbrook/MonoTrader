using System;
using Common.Logging;

namespace TangleTrading.Strategy
{
    public static class StrategyPlatformExtensions
    {
        public static ILog GetLogger(this IStrategy stgy)
        {
            return LogManager.GetLogger(stgy.GetType().FullName);
        }
        //TODO: logger

        public static void Hint(this IStrategy stgy, string message,bool ifShout=false)
        {

        }
    }
}
