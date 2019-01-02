using MonoTrader.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTrader.Future
{
    public enum PositionEffect
    {
        /// <summary>
        /// 开仓
        /// </summary>
        OPEN = 1,

        /// <summary>
        /// 平仓
        /// </summary>
        CLOSE =2
    } 

    public class Order:OrderBase
    {

    }
}
