using Akka.Actor;
using TangleTrading.Adapter;
using TangleTrading.Framework.Command;

namespace TangleTrading.Framework
{
    public class AdapterActor : ReceiveActor
    {
        private IBroker Broker { get; set; }
        public AdapterActor(IBroker broker, dynamic config = null)
        {
            Broker = broker;
            Broker.Initialize(config);

            if (config.SyncTickMillseconds > 0)
            {
               // Context.System.Scheduler.ScheduleTellRepeatedly(0, config.SyncTickMillseconds, Self, "SYNCTICK", Self);
            }

            Receive<AdapterCommand>((AdapterCommand arg) => { });

            //Broker.Start();
        }




    }
}
