using System;
using Tangle.Trading.Base;

namespace Tangle.Trading
{
    public interface IAdapter
    {
        /// <summary>
        /// 初始化
        /// 有多个角色时，也只初始化一次
        /// </summary>
        /// <param name="param">Parameter.</param>
        void Initialize(dynamic param);


    }
}