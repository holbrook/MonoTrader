using System;
namespace Tangle.Trading
{
    public interface IBrokerMeta
    {
        Type BrokerType { get;}

        /// <summary>
        /// 交易适配器支持的市场代码。
        /// 一个适配器可以支持多个，
        /// XSHG :上海证券交易所，
        /// XSHE :深圳证券交易所，
        /// CCFX :中国金融期货交易所，
        /// XDCE :大连商品交易所，
        /// XSGE :上海期货交易所，
        /// XZCE :郑州商品交易所。
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
        string[] Exchanges { get; }
    }
}
