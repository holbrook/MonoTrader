using System;
using System.Collections.Generic;

namespace TangleTrading.Adapter
{
    public interface IBrokerMeta
    {
        Dictionary<Type,string[]> Exchanges { get; }
        //Type BrokerType { get;}

        // 上海证券交易所（英文：Shanghai Stock Exchange） SHSE
        // 深圳证券交易所 Shenzhen Stock Exchange        SZSE
        // 大连商品交易所（英文名称为Dalian Commodity Exchange，英文缩写为DCE）
        // 上海期货交易所(Shanghai Futures Exchange，缩写为SHFE)
        // 中国金融期货交易所 中国金融期货交易所(China Financial Futures Exchange,缩写CFFEX) 
        // 郑商所 ZCE   CZCE

        /// <summary>
        /// 交易适配器支持的市场代码。
        /// 一个适配器可以支持多个，
        /// XSHG :上海证券交易所，
        /// XSHE :深圳证券交易所，
        /// CCFX :中国金融期货交易所，
        /// XDCE :大连商品交易所，
        /// XSGE :上海期货交易所，
        /// XZCE :郑州商品交易所。
        /// 
        /// 市场中文名   市场代码

        //        上交所 SHSE
        //深交所 SZSE
        //中金所 CFFEX
        //上期所 SHFE
        //大商所 DCE
        //郑商所 CZCE
        //上海国际能源交易中心 INE


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
        //string[] Exchanges { get; }
    }
}
