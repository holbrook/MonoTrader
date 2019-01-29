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

            string a = broker.AddOrder("600000.XSHE", 100, Tangle.Trading.Base.ORDER_SIDE.BUY, 8000);
            var orders = broker.GetOpenOrders();
        }
    }
}
