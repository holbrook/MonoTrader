using System;
using Tangle.PluginModel;

namespace Tangle.Trading.Core
{
    class AdapterCommand
    {
        public string Command { get; private set; }
        public AdapterCommand(string cmd)
        {
            Command = cmd;
        }
    }

    [PartAttribute("适配器")]
    public class AdapterPart : IPart
    {
        public IAdapter adapter;

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
