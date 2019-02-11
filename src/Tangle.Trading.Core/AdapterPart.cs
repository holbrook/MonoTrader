using System;
using Tangle.PluginModel;

namespace Tangle.Trading.Core
{
    [PartAttribute("适配器")]
    public class AdapterPart : IPart
    {

        public IPartContext Context { get; set; }

        public void Initialize(dynamic param)
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            Context.Schedule(0, 4000, "GetTick");
            Context.Schedule(0, 2000, "SyncAccount");
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
