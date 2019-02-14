using TangleTrading.Framework.Util;
using TangleTrading.Future;
using TangleTrading.RootNetAdapter;
using TangleTrading.Stock;

namespace TestRootNetAdapter
{
    class Program
    {
        static void Main(string[] args)
        {
            RootNetConfig config = new RootNetConfig();

            config.OptID = "99990";       // 柜员代码
            config.OptPwd = "112233";     // 柜员口令
            config.OptMode = "W5";        // 委托方式

            BrokerConfig stockConfig = new BrokerConfig();
            stockConfig.BrokerType = typeof(IStockBroker);// "StockBroker";
            stockConfig.AccountID = "001653019819";
            stockConfig.AccountPwd = "135246";

            stockConfig.Accounts.Add(new AccountConfig("XSHG", "D890019819", "135246"));    //上交所
            stockConfig.Accounts.Add(new AccountConfig("XSHE", "0030605790", "135246"));    //上交所

            config.Brokers.Add(stockConfig);


            BrokerConfig futureConfig = new BrokerConfig();
            futureConfig.BrokerType = typeof(IFutureBroker);
            futureConfig.AccountID = "000000013856";
            futureConfig.AccountPwd = "135246";

            futureConfig.Accounts.Add(new AccountConfig("CCFX", "02088981", "135246"));    //中金所
            config.Brokers.Add(futureConfig);

            ConfigUtil.Save(config, "rncfg.xml");
            RootNetConfig config = ConfigUtil.Load<RootNetConfig>("rncfg.xml");

            RootNetBroker broker = new RootNetBroker();

           

            broker.Initialize(config);

            //broker.TestOrder();

            //股票下单
            //string a = broker.AddStockOrder("600000.XSHG", 100, Tangle.Trading.Base.ORDER_SIDE.BUY, 11.6m);
            //string a = broker.AddStockOrder("000001.XSHE", 100, Tangle.Trading.Base.ORDER_SIDE.BUY, 14m);

            //股票查询订单
            //var orders = broker.GetOpenStockOrders();

            //股票撤单
            //broker.CancelStockOrder("00000024");
            //orders = broker.GetOpenStockOrders();

            //Console.WriteLine(orders.Count);
            //股票同步数据（资金，仓位）
            //broker.SynchronizeStockAccount();

            //期货下单
            //
            broker.AddFutureOrder("IC1903.CCFX", 1, TangleTrading.Base.ORDER_SIDE.BUY, TangleTrading.Future.POSITION_EFFECT.OPEN);
            //期货查询订单

            //期货撤单

            //期货同步数据



        }


    }
}
