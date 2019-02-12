using System;
using Akka.Actor;
using Tangle.Trading.Core;
using TangleTrading.Model;

namespace TangleTrading.Framework
{
    public class AdapterActor : ReceiveActor
    {
        private IAdapter Adapter { get; set; }
        public AdapterActor(IAdapter adapter, dynamic config=null)
        {
            Adapter = adapter;
            Adapter.Initialize(config);

            if(config.SyncTickMillseconds>0)
            {
                Context.System.Scheduler.ScheduleTellRepeatedly(
                )
            }

            Receive<AdapterCommand>((AdapterCommand arg) => { });

            Adapter.Start();
        }
    }
}
