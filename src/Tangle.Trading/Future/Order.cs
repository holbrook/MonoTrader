using Tangle.Trading.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tangle.Trading.Future
{
    public enum POSITION_EFFECT
    {
        /// <summary>
        /// 开仓
        /// </summary>
        OPEN = 1,

        /// <summary>
        /// 平仓
        /// </summary>
        CLOSE =2,

        /// <summary>
        /// 平今仓
        /// </summary>
        CLOSE_TODAY = 3,

        /// <summary>
        /// 平昨
        /// </summary>
        CLSOE_YESTERDAY = 4,


        /// <summary>
        /// 强平
        /// </summary>
        FORCE_CLOSE = 9,


        UN_SUPPORT = 0
    };



    public class Order:OrderBase
    {

    }
}
