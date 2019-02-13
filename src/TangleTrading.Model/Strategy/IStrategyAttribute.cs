using System;
namespace TangleTrading.Strategy
{
    /// <summary>
    /// <see cref="IStrategy"/>的属性定义。MonoFramework通过IMonoPart的属性生命发现和识别插件。
    /// </summary>
    public interface IStrategyAttribute
    {
        string Name { get; }
    }
}
