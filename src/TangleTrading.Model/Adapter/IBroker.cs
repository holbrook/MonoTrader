using System;
using TangleTrading.Base;

namespace TangleTrading.Adapter
{
    public interface IBroker
    {
        /// <summary>
        /// 初始化
        /// 有多个角色时，也只初始化一次
        /// </summary>
        /// <param name="param">Parameter.</param>
        void Initialize(dynamic param);


    }
}