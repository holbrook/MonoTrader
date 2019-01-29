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
        CLOSE_TODAY = 3
        //CLSOE_YESTERDAY = 4
    };

    public class Order:OrderBase
    {

    }
}
