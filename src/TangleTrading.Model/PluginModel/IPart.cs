using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tangle.PluginModel
{
    /// <summary>
    /// Part(部件)，是 MonoFramework 中的逻辑扩展
    /// Part实现了CQRS架构中的Aggregate，会接收(Execute)Command，更新Domain后发出Event或Exception;
    /// 也会接收(Handle）其他Part发出的Event通知。
    /// 
    /// Framework会将每个 Part 包装成一个 Actor，当 Actor 接收到消息(Command / Event)时，调用 Part 中对应的方法
    /// Actor 同时作为 Part 的 Context, Part 通过这个 Actor 发送Event / Exception
    /// </summary>
    public interface IPart
    {
        IPartContext Context { get; set; }
        void Initialize(dynamic param);
        void Start();
        void Stop();
    }
}
