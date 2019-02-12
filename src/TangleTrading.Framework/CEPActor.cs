using Akka.Actor;
using com.espertech.esper.client;
using Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TangleTrading.Framework
{
    class CEPActor : ReceiveActor
    {
        ILog Logger = LogManager.GetLogger(typeof(CEPActor));
        private Configuration _esperConfig;

        /* 创建statement的管理接口实例 */
        private EPAdministrator _esperAdmin;
        
        ///* 引擎实例运行接口，负责为引擎实例接收数据并发送给引擎处理 */
        //private EPRuntime _esperRuntime;

        public CEPActor()
        {
            _esperConfig = new Configuration();
            /* 创引擎实例 */
            EPServiceProvider _provider = EPServiceProviderManager.GetDefaultProvider(_esperConfig);
            _esperAdmin = _provider.EPAdministrator;


            Receive<Type>((type) =>
            {
                _esperAdmin.Configuration.AddEventType(type);
                AddCEPListener("select ID,Price from Apple", new UpdateEventHandler((sender, e) =>
                {
                    if (null == e.NewEvents)
                        return;
                    foreach (EventBean eb in e.NewEvents)
                    {   
                        Logger.Info(string.Format("RECEIVED Apple  name:{0}, price:{1}", eb.Get("ID"), eb.Get("Price")));
                    }
                }));
            });

            

      

            

            Receive<object>((msg) => {
                
                Console.WriteLine("CEPActor received {0}", msg.GetType().FullName);
                //var x = _esperConfig.EventTypes;//..Contains(msg.GetType());
                //_provider.EPRuntime.SendEvent(new Orange());
                _provider.EPRuntime.SendEvent(msg);

                

            });
        }

        public void AddCEPListener(string epl, UpdateEventHandler handler)
        {
            //创建EPL查询语句实例，功能：查询所有进入的myEvent事件
            EPStatement statement = _esperAdmin.CreateEPL(epl);
            statement.AddEventHandlerWithReplay(handler);
            

        }

        
    }
}
