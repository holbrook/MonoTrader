using Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tangle.PluginModel
{
    public interface IPartContext
    {
        //ILog log = ILogManager.GetLogger("Program");
        ILog Logger { get; }
        ILog GetLogger(string name);

        void Publish(object message);
        void Subscribe(Type t);


        void Schedule(int initialMillisecondsDelay, int millisecondsInterval, object message);

        /// <summary>
        /// 订阅CEP事件
        /// </summary>
        /// <param name="eplString"></param>
        /// <returns>事件ID。通过ID区分定义的多个事件类型</returns>
        string ObserveComplexEvent(string eplString);
    }
}
