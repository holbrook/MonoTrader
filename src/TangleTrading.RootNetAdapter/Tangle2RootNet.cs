﻿using System;
using TangleTrading.Model.Base;
using TangleTrading.Model.Future;

namespace TangleTrading.RootNetAdapter
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
                    return "UN_SUPPORT";
            }
        }

        public static string TransF_offSetFlag(POSITION_EFFECT effect)
        {
            switch (effect)
            {
                case POSITION_EFFECT.OPEN:
                    return "OPEN";

                case POSITION_EFFECT.CLOSE:
                    return "CLOSE";

                case POSITION_EFFECT.CLOSE_TODAY:
                    return "CLOSETD";

                case POSITION_EFFECT.CLSOE_YESTERDAY:
                    return "CLOSEYD";

                case POSITION_EFFECT.FORCE_CLOSE:
                    return "FCLOSE";

                default:
                    return "UN_SUPPORT";
            }
        }



        public static string OrderbookID2stkId(string orderbookID)
        {
            return orderbookID.Split('.')[0];
        }
     
        public static string OrderbookID2exchId(string orderbookID)
        {
            return TransExchID(orderbookID.Split('.')[1]);
        }

        public static string TransOrderType(ORDER_SIDE side)
        {
            switch(side)
            {
                case ORDER_SIDE.BUY:
                    return "B";
                case ORDER_SIDE.SELL:
                    return "S";
                default:
                    return "UNKNOWN";
            }
        }


    }
}
