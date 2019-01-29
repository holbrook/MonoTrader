using Tangle.PluginModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin1
{
    [PartAttribute("部件2")]
    public class Part2InPlugin1 : IPart, IHandle<Apple>
    {
        public IPartContext Context { get; set; }

        public void Handle(Apple e)
        {
            Context.Logger.Info("Part2 received Apple");
        }

        public void Initialize(dynamic param)
        {
            //throw new NotImplementedException();
        }

        public void Start()
        {
            //throw new NotImplementedException();
        }

        public void Stop()
        {
            //throw new NotImplementedException();
        }
    }
}
