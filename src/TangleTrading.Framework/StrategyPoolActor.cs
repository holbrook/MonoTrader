using Akka.Actor;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TangleTrading.Strategy;

namespace TangleTrading.Framework
{
    public class StrategyPoolActor:ReceiveActor
    {
        [ImportMany]
        //public List<Lazy<IFoo, IFooMultiMeta>> Foos { get; set; }
        public List<IStrategy> Strategies { get; set; }

        public void Initialize(dynamic param)
        {
            //https://docs.microsoft.com/zh-cn/dotnet/framework/mef/attributed-programming-model-overview-mef#custom-export-attributes
            //https://stackoverflow.com/questions/17458304/custom-mef-exportattribute-with-allowmultiple-true-causes-duplication
            //new CompositionContainer(new AssemblyCatalog(Assembly.GetExecutingAssembly())).ComposeParts(this);
            foreach (var b in Strategies)
            {
                Console.WriteLine(b.GetType() + b.GetHashCode().ToString());
            }
        }
    }
}
