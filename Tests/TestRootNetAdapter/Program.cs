using System;
using TangleTrading.Adapter;
using TangleTrading.Base;
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
            stockConfig.BrokerType = typeof(IStockBroker).Name;// "StockBroker";
            stockConfig.AccountID = "001653019819";
            stockConfig.AccountPwd = "135246";

            stockConfig.Accounts.Add(new AccountConfig("XSHG", "D890019819", "135246"));    //上交所
            stockConfig.Accounts.Add(new AccountConfig("XSHE", "0030605790", "135246"));    //上交所

            config.Brokers.Add(stockConfig);


            BrokerConfig futureConfig = new BrokerConfig();
            futureConfig.BrokerType = typeof(IFutureBroker).Name;
            futureConfig.AccountID = "000000013856";
            futureConfig.AccountPwd = "135246";

            futureConfig.Accounts.Add(new AccountConfig("CCFX", "02088981", "135246"));    //中金所
            config.Brokers.Add(futureConfig);

            ConfigUtil.Save(config, "rncfg.xml");
            
            config = ConfigUtil.Load<RootNetConfig>("rncfg.xml");

            RootNetFeeder feeder = new RootNetFeeder();
            feeder.Initialize(config);

            feeder.Subscribe("512160.XSHG");
            feeder.Subscribe("113011.XSHG");
            feeder.Subscribe("128024.XSHE");
            feeder.Subscribe("IC1903.CCFX");
            feeder.Subscribe("IF1903.CCFX");

            feeder.FeedEventHandler += new EventHandler<FeedEventArgs>((sender, e) =>
            {
                if(e.Event is TangleTrading.Stock.Tick)
                {
                    Console.WriteLine(string.Format("stock Tick:{0}", e.Event));
                    return;
                }

                if (e.Event is TangleTrading.Future.Tick)
                {
                    Console.WriteLine(string.Format("future Tick:{0}", e.Event));
                    return;
                }
            });


            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Enabled = true;
            timer.Interval = 500;//执行间隔时间,单位为毫秒
           


            timer.Elapsed += new System.Timers.ElapsedEventHandler((sender,arg)=> {
                feeder.Go();
            });

            timer.Start();
            Console.ReadLine();

            //RootNetBroker broker = new RootNetBroker();



            //broker.Initialize(config);

            //broker.TestOrder();

            //股票下单
            //var a = broker.AddStockOrder("600000.XSHG", 100, ORDER_SIDE.BUY, 11.6m);
            //var b = broker.AddStockOrder("000001.XSHE", 100, ORDER_SIDE.BUY, 14m);

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
            //broker.AddFutureOrder("IF1903.CCFX", 1, TangleTrading.Base.ORDER_SIDE.BUY, TangleTrading.Future.POSITION_EFFECT.OPEN);

            //期货查询订单
            //broker.GetOpenFutureOrders();
            //期货撤单

            //期货同步数据



        }

        private static void Feeder_FeedEventHandler(object sender, TangleTrading.Adapter.FeedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
