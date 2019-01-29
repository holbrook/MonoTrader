using System;
namespace Tangle.Trading.RootNetAdapter
{
    public static class Tangle2RootNet
    {
        public static string TransExchID(string exchange)
        {
            switch (exchange)
            {
                case "XSHG":       //   沪A,上海证券交易所
                    return "0";
                case "XSHE":       //   深A,深圳证券交易所
                    return "1";
                case "NEEQ":       //   特A(新三板)
                    return "6";
                case "CCFX":       // 中金所,中国金融期货交易所
                    return "F";
                case "XSGE":       //  上期所,上海期货交易所
                    return "S";
                case "XDCE":       //  大商所,大连商品交易所
                    return "D";
                case "XZCE":       //  郑商所,郑州商品交易所
                    return "Z";
                default:
                    return "UNSUPPORT";
            }
        }
    }
}
