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

            //股票下单
            string a = broker.AddStockOrder("600000.XSHG", 100, Tangle.Trading.Base.ORDER_SIDE.BUY, 8000);

            //股票查询订单
            var orders = broker.GetOpenStockOrders();

            //股票撤单
            broker.CancelFutureOrder(a);

            //股票同步数据（资金，仓位）


            //期货下单

            //期货查询订单

            //期货撤单

            //期货同步数据

        }


    }
}
