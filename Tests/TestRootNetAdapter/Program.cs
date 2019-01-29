using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tangle.Trading.RootNetAdapter;

namespace TestRootNetAdapter
{
    class Program
    {
        static void Main(string[] args)
        {

            RootNetBroker broker = new RootNetBroker();
            broker.Initialize(null);
            var orders = broker.GetOpenOrders();
        }
    }
}
