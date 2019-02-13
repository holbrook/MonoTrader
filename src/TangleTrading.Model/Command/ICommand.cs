using System;
namespace TangleTrading.Command
{
    /// <summary>
    /// 仅仅是标记接口(Marker Interface).
	/// <see cref="IPart"/>通过实现<see cref="IExecute"/>接口，声明自己可以处理一个ICommand.
    /// ICommand来自其他Actor（UI通过BridgeActor发布消息）
    /// Command 执行后，会返回一个执行结果<see cref="ExecuteStatus"/>
    /// </summary>
    public interface ICommand
    {
    }

    /// <summary>
    /// 可以自定义执行结果的类型
    /// </summary>
    public interface ICommand<T> : ICommand
        where T: ExecuteStatus
    {

    }
}
