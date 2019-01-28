using System;
using Tangle.Trading.Base;

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
        /// XSHG表示上海证券交易所，
        /// XSHE表示深圳证券交易所，
        /// CCFX表示中国金融期货交易所，
        /// XDCE表示大连商品交易所，
        /// XSGE表示上海期货交易所，
        /// XZCE表示郑州商品交易所。
        /// exchId/Grantexchlist    市场代码
                //0   沪A
                //2   沪B
                //1   深A
                //3   深B
                //4   深港通
                //5   沪港通
                //6   特A(新三板)
                //F 中金所
                //S 上期所
                //D 大商所
                //Z 郑商所
                //X 沪期权
                //Y 深期权
        /// </summary>
        /// <value>The markets.</value>
        string[] Markets { get; }

        AccountBase Account { get; }

        void SynchronizeAccount();
    }
}
