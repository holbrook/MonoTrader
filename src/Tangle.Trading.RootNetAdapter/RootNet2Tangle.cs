using System;
using Tangle.Trading.Base;
using Tangle.Trading.Future;

namespace Tangle.Trading.RootNetAdapter
{
    public static class RootNet2Tangle
    {
        public static string TransExchID(string rnID)
        {
            switch (rnID)
            {
                case "0":       //   沪A,上海证券交易所
                    return "XSHG";
                case "1":       //   深A,深圳证券交易所
                    return "XSHE";
                case "6":       //   特A(新三板)
                    return "NEEQ";
                case "F":       // 中金所,中国金融期货交易所
                    return "CCFX";
                case "S":       //  上期所,上海期货交易所
                    return "XSGE";
                case "D":       //  大商所,大连商品交易所
                    return "XDCE";
                case "Z":       //  郑商所,郑州商品交易所
                    return "XZCE";
                case "2":   //   沪B
                case "3":    //   深B
                case "4":   //   深港通
                case "5":   //沪港通
                case "X":   // 沪期权
                case "Y":   //深期权
                default:
                    return "UNSUPPORT";
            }
        }

        public static POSITION_EFFECT TransPositionEffect(string flag)
        {
            switch (flag)
            {
                case "OPEN":
                    return POSITION_EFFECT.OPEN;
                case "CLOSE":
                    return POSITION_EFFECT.CLOSE;
                case "CLOSETD":
                    return POSITION_EFFECT.CLOSE_TODAY;
                case "CLOSEYD":
                    return POSITION_EFFECT.CLSOE_YESTERDAY;
                case "FCLOSE":
                    return POSITION_EFFECT.FORCE_CLOSE;
                default:
                    return POSITION_EFFECT.UN_SUPPORT;

            }
        }


        public static ORDER_SIDE TransOrderType(string type)
        {
            //TODO: B, 0B 有什么区别？
            switch (type)
            {
                case "B":
                case "0B":
                    return ORDER_SIDE.BUY;
                case "S":
                case "0S":
                    return ORDER_SIDE.SELL;
                default:
                    return ORDER_SIDE.UNKNOWN;
            }
        }
    }
}
