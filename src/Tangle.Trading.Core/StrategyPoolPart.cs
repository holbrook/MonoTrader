using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Tangle.PluginModel;

namespace Tangle.Trading.Core
{
    [PartAttribute("策略池")]
    public class StrategyPoolPart : IPart
    {

        [ImportMany]
        //public List<Lazy<IFoo, IFooMultiMeta>> Foos { get; set; }
        public List<IAdapter> Brokers { get; set; }

        public IPartContext Context { get; set; }

        public void Initialize(dynamic param)
        {
            //https://docs.microsoft.com/zh-cn/dotnet/framework/mef/attributed-programming-model-overview-mef#custom-export-attributes
            //https://stackoverflow.com/questions/17458304/custom-mef-exportattribute-with-allowmultiple-true-causes-duplication
            //new CompositionContainer(new AssemblyCatalog(Assembly.GetExecutingAssembly())).ComposeParts(this);
            foreach (var b in Brokers)
            {
                Console.WriteLine(b.GetType()+b.GetHashCode().ToString());
            }
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
