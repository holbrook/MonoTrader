using System;
using System.ComponentModel.Composition;

namespace TangleTrading.Model
{
    /// <summary>
    /// IStrategy 的描述,包括：
    /// SubscribeTypes: 订阅消息类型。<see cref="IStrategy"/>的 Context 根据这个属性调用 Part 的 OnMessage()方法。
    /// 具体的 topic name 由 Context 决定
    /// PublishTypes: 发布消息类型
    /// [PartAttribute(new int[] { 3, 4, 5 })]
    /// </summary>    

    [MetadataAttribute]
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class StrategyAttribute: ExportAttribute, IStrategyAttribute
    {
        public string Name { get; private set; }
        //public Type[] SubscribeTypes { get; set; }
        //public Type[] PublishTypes { get; set; }
        public StrategyAttribute(string name)//, Type[] subscribeTypes, Type[] publishTypes)
            :base(typeof(IStrategy))
        {
            Name = name;
            //SubscribeTypes = subscribeTypes;
            //PublishTypes = publishTypes;
        }
    }
}
