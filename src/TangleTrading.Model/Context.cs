using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TangleTrading.Model.Base;

namespace TangleTrading.Model
{
    /// <summary>
    /// 策略上下文
    /// </summary>
    public class Context : DynamicObject
    {
        /// <summary>
        /// 策略相关的证券数组。里面所有证券的行情、成交回报等信息会被订阅。
        /// </summary>
        /// <value>The securities.</value>
        public string[] Securities { get; set; }

        public Portfolio Portfolio { get; private set; }


        public dynamic CustomData = new ExpandoObject();

    }
}
