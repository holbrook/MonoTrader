using System;
namespace Tangle.Trading
{
    public interface IBroker
    {
        /// <summary>
        /// 交易适配器支持的市场代码。
        /// 一个适配器可以支持多个，比如INDX    1
        ///    SGEX    1
/// XSHE    1
/// XSHG    1
        /// </summary>
        /// <value>The markets.</value>
        string[] Markets { get; }
    }
}
