using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Dynamic;
using Tangle.Trading.Base;
using Tangle.Trading.Future;
using Tangle.Trading.Stock;

namespace Tangle.Trading.RootNetAdapter
{
    [Export(typeof(IBroker))]
    [Broker(typeof(IStockBroker), new string[] { "XSHE", "XSHG", "NEEQ" })]
    [Broker(typeof(IFutureBroker), new string[] { "CCFX", "XDCE", "XSGE", "XZCE" })]
    public class RootNetBroker : IBroker, IStockBroker, IFutureBroker
    {
        private GWDPApiCS.GWDPApiCS oPackage;
        private dynamic commonParams = new ExpandoObject();

        public Stock.Account StockAccountInfo { get; private set; }
        public Future.Account FutureAccountInfo { get; private set; }

        public RootNetBroker()
        {
            StockAccountInfo = new Stock.Account();
            FutureAccountInfo = new Future.Account();
        }

        public void Initialize(dynamic param)
        {
            HardwareInfo info = new HardwareInfo();

            //commonParams.terminalInfo = string.Format(  // 终端信息 Y   
            //"PC;IIP:{0}; LIP:{1}; MAC:{2}; HD:{3}; PCN:{4};CPU:{5}; PI:{6}",
            //info.IpAddress,
            //info.IpAddress,
            //info.MacAddress,
            //info.DiskID,
            //info.ComputerName,
            //info.CpuID,
            //"XXX"
            //);

            commonParams.permitMac = info.MacAddress;

            commonParams.optId = "99990";       // 柜员代码
            commonParams.optPwd = "112233";     // 柜员口令
            commonParams.optMode = "A1";        // 委托方式
            commonParams.acctId = "001653019819";        //现货资金帐号
            commonParams.tradePwd = "135246";     // 现货资金密码
            commonParams.regId = "0030605790";   //深圳股东代码
            //commonParams.regId = "D890019819";   //上海股东代码
            

            //TODO: 查询股东账号, 考虑两个市场？

            //TODO: 同步一次账号

            oPackage = new GWDPApiCS.GWDPApiCS();

            //初始化。一个对象初始化一次就可以
            if (oPackage.Init() == false)
            {
                //Logger.Error("初始化通讯对象失败！");
                return;
            }
        }



        public void Start()
        {
            //TODO
        }

        public void Stop()
        {
            //清理运行环境，不需要每次交互后都调用，退出时调用即可
            oPackage.UnInit();
        }


        public void SynchronizeStockAccount()
        {
            _synchronizeStockAccount();
            _synchronizeStockPosition();
        }

        private void _synchronizeStockAccount()
        {
            oPackage.ClearSendPackage();

            oPackage.SetFunctionCode("00800140");   //资金查询
            oPackage.SetFlags(0);

            oPackage.SetValue(1, "optId", commonParams.optId);                  //柜员代码
            oPackage.SetValue(1, "optPwd", commonParams.optPwd);                //柜员密码
            oPackage.SetValue(1, "optMode", commonParams.optMode);              //委托方式
            //oPackage.SetValue(1, "permitMac", commonParams.permitMac);          //登录Mac地址
            //oPackage.SetValue(1, "terminalInfo", commonParams.terminalInfo);                //终端信息

            oPackage.SetValue(1, "acctId", commonParams.acctId);                  // 资金账号
            oPackage.SetValue(1, "tradePwd", commonParams.tradePwd);                   //交易密码 Y

            oPackage.SetValue(1, "maxRowNum", "500");                   //每次返回的最大记录数  Y 取值范围：1～500
            oPackage.SetValue(1, "packNum", "1");                   //查询序号    Y 首次查询时为1，查下一页时递加1

            //custid  客户代码 N
            //currencyId 货币代码    Y
            //regId   股东代码 N  二选一输入
            //flag    参考市值中体现当日逆回购数据标志 N   1 - 体现当日逆回购数据；其他值 - 不体现当日逆回购数据


            bool ret = oPackage.ExchangeMessage();

            var x = oPackage.GetValue(0, "successflg" +
                "");
            var c = oPackage.GetValue(0, "errorcode");
            var y = oPackage.GetValue(0, "failinfo");
            Console.WriteLine(string.Format("Error {0}:{1}", oPackage.GetValue(0, "errorCode"), oPackage.GetValue(0, "failInfo")));

            //获取返回记录条数
            int iCnt = int.Parse(oPackage.GetValue(0, "recordCnt"));

            StockAccountInfo.Cash = decimal.Parse(oPackage.GetValue(1, "usableAmt"));   //可用金额
            StockAccountInfo.FrozenCash =
                decimal.Parse(oPackage.GetValue(1, "tradeFrozenAmt")) +   //交易冻结
                decimal.Parse(oPackage.GetValue(1, "exceptFrozenAmt"));   //异常冻结
            StockAccountInfo.MarketValue = decimal.Parse(oPackage.GetValue(1, "currentStkValue"));   //参考市值        动态市值，包括未到期融券回购资产和场外开放式基金资产
            StockAccountInfo.DividendReceivable = 0m;
            StockAccountInfo.TransactionCost = 0m;

            //currencyId 货币代码
            //custName 帐户姓名
            //stkvalue 证券市值
            //currentStkValue 
            //currentAmt  当前资金余额
            //currentAmtForAsset  实时资金余额 考虑实时买卖
            //fundInAm 资金存入净值
            //stkInAmt 证券转入净值
            //profitAndLoss 投资损益
            //OTCcurrentStkValue 场外开放基金参考市值
            //HK_usableAmt 港股通可用金额
            //str1 资金帐户属性      翻译后内容
        }


        private void _synchronizeStockPosition()
        {
            oPackage.ClearSendPackage();

            oPackage.SetFunctionCode("00300021");   //股份查询（建议交易查询时使用，效率高） 
            oPackage.SetFlags(0);

            oPackage.SetValue(1, "optId", commonParams.optId);                  //柜员代码
            oPackage.SetValue(1, "optPwd", commonParams.optPwd);                //柜员密码
            oPackage.SetValue(1, "optMode", commonParams.optMode);              //委托方式
            oPackage.SetValue(1, "permitMac", commonParams.permitMac);          //登录Mac地址
            //oPackage.SetValue(1, "terminalInfo", commonParams.terminalInfo);                //终端信息

            oPackage.SetValue(1, "acctId", commonParams.acctId);                  // 资金账号
            oPackage.SetValue(1, "tradePwd", commonParams.tradePwd);                   //交易密码 Y

            //custid  客户代码 N
            //exchId 交易市场代码  N 送股东代码的必须送市场
            //regId 股东代码                    N
            //stkId 证券代码    N
            //stkType 证券类别 N


            bool ret = oPackage.ExchangeMessage();

            var x = oPackage.GetValue(0, "successflg");
            var c = oPackage.GetValue(0, "errorcode");
            var y = oPackage.GetValue(0, "failinfo");
            Console.WriteLine(string.Format("Error {0}:{1}", oPackage.GetValue(0, "errorCode"), oPackage.GetValue(0, "failInfo")));

            StockAccountInfo.Positions.Clear();

            //获取返回记录条数
            int iCnt = int.Parse(oPackage.GetValue(0, "recordCnt"));


            //逐条获取返回的结果
            for (int i = 1; i <= iCnt; i++)
            {
                string orderbookID = oPackage.GetValue(i, "stkId") + "." +
                RootNet2Tangle.TransExchID(oPackage.GetValue(i, "exchId"));

                Stock.Position position = new Stock.Position();
                position.OrderbookID = orderbookID;
                position.OrderbookName = oPackage.GetValue(i, "stkName");
                position.Quantity = int.Parse(oPackage.GetValue(i, "currentQty"));//  股分余额
                position.MarketValue = decimal.Parse(oPackage.GetValue(i, "stkValue"));// 证券市值
                position.AvgPrice = decimal.Parse(oPackage.GetValue(i, "previousCost"));// 参考成本
                position.Profit = decimal.Parse(oPackage.GetValue(i, "previousIncome"));// 参考收益
                position.TodayQuantity = position.Quantity -
                    int.Parse(oPackage.GetValue(i, "usableQty"));// 今仓 = 持仓-可用

                //regId                   股东代码 汇总不送
                //regName 股东姓名        汇总不送
                //previousQty 昨日余额
                //exceptFrozenQty 异常冻结
                //unsaleableQty 非流通余额

                StockAccountInfo.Positions.Add(orderbookID, position);

            }
        }


        public void TestOrder()
        {
            oPackage.ClearSendPackage();
            oPackage.SetFunctionCode("00100030");
            oPackage.SetFlags(0);

            oPackage.SetValue(0, "recordCnt", "1");

            oPackage.SetValue(1, "optId", "99990");

            //设置柜员密码
            oPackage.SetValue(1, "optPwd", "112233");

            //设置委托方式
            oPackage.SetValue(1, "optMode", "W5");

            //设置市场代码
            oPackage.SetValue(1, "exchId", "0");

            //设置股东代码
            oPackage.SetValue(1, "regId", "D890019819");

            //设置交易密码
            oPackage.SetValue(1, "tradePwd", "135246");

            //设置证券代码
            oPackage.SetValue(1, "stkId", "600030");

            //设置买卖方向
            oPackage.SetValue(1, "orderType", "B");

            //设置委托数量
            oPackage.SetValue(1, "orderQty", "100");

            //设置委托价格
            oPackage.SetValue(1, "orderPrice", "19");

            //设置MAC地址
            oPackage.SetValue(1, "permitMac", "E3A4D7CBF6AF");

            //和网关交互
            if (oPackage.ExchangeMessage() == false)
            {
                Console.WriteLine("委托请求（00100211）交互失败");                
                return;
            }

            //获取成功失败标志
            if (oPackage.GetValue(0, "successflg").Equals("0") == false)
            {//失败
                Console.WriteLine(string.Format("委托失败，错误代码：{0}，错误信息{1}", oPackage.GetValue(0, "errorcode"), oPackage.GetValue(0, "failinfo")));                
                return;
            }

            //获取返回记录条数
            int iCnt = int.Parse(oPackage.GetValue(0, "recordCnt"));
            //获取返回的合同序号
            string sContractNum = oPackage.GetValue(1, "contractNum");


        }
        public string AddStockOrder(string orderbookID, int quantity, ORDER_SIDE side, decimal price)
        {

            oPackage.ClearSendPackage();

            oPackage.SetFunctionCode("00100030");   //普通买卖委托
            oPackage.SetFlags(0);

            oPackage.SetValue(1, "optId", commonParams.optId);                  //柜员代码
            oPackage.SetValue(1, "optPwd", commonParams.optPwd);                //柜员密码
            oPackage.SetValue(1, "optMode", commonParams.optMode);              //委托方式
            oPackage.SetValue(1, "permitMac", commonParams.permitMac);          //登录Mac地址
            //oPackage.SetValue(1, "terminalInfo", commonParams.terminalInfo);                //终端信息

            oPackage.SetValue(1, "regId", commonParams.regId);                  // 股东代码 N
            oPackage.SetValue(1, "tradePwd", commonParams.tradePwd);                   //交易密码 Y
            oPackage.SetValue(1, "exchId", Tangle2RootNet.OrderbookID2exchId(orderbookID)); //市场代码 Y
            oPackage.SetValue(1, "stkId", Tangle2RootNet.OrderbookID2stkId(orderbookID));     //证券代码    Y 转股回售、权证行权时不使用这个字段
            oPackage.SetValue(1, "orderType", Tangle2RootNet.TransOrderType(side));   //交易类型 Y   JB - 行权，KS - ETF赎回，IS - 场内开放式基金可赎回数量
            oPackage.SetValue(1, "orderPrice", (0m == price) ? "0" : price.ToString()); //委托价格    Y 市价委托时固定送0
            oPackage.SetValue(1, "orderQty", quantity.ToString());   //委托数量    Y
                                                                     //oPackage.SetValue(1, "permitPhone", "123456789");   //主叫电话

            //basicStkId 基础证券代码 N 转股回售时送可转债代码, 权证行权时送权证代码，其他委托时使用stkId
            //permitMac   登录Mac地址 N
            //permitPhone 电话委托的主叫号码   N
            //pricestrategy   冻结价格标识 N   "S1-卖1,S2-卖2,S3-卖3,S4-卖4,S5-卖5，ZT-涨停"
            //creditTradeType 信用交易标志  N
            //custid  客户代码 N
            //collectorid 收购人代码   N 要约收购业务
            //exteriorAcctId 外部帐号    N
            //contractNum 合同序号 N   实际使用时需要和柜台约定合同号区间，并保证唯一性
            //ClientId    策略Id N
            //isSolicited 是否接受了投资顾问   N Y-是 / N - 否
            //oPackage.SetValue(0, "recordCnt", "1"));                  //设置请求记录数

            bool ret = oPackage.ExchangeMessage();

            var x = oPackage.GetValue(0, "successflg");
            var c = oPackage.GetValue(0, "errorcode");
            var y = oPackage.GetValue(0, "failinfo");
            Console.WriteLine(string.Format("Error {0}:{1}", oPackage.GetValue(0, "errorCode"), oPackage.GetValue(0, "failInfo")));

            //返回合同号
            var no = oPackage.GetValue(1, "contractNum");
            return no;
        }

        public void CancelStockOrder(string orderID)
        {
            string orderbookID = null;

            List<Stock.Order> orders = GetOpenStockOrders();
            foreach (var order in orders)
            {
                if (order.OrderID == orderID)
                {
                    orderbookID = order.OrderbookID;
                    break;
                }
            }

            if (null == orderbookID)
            {
                Console.WriteLine("未找到可撤的订单:" + orderID);
                return;
            }

            oPackage.ClearSendPackage();

            oPackage.SetFunctionCode("00100110");   //批量撤单委托
            oPackage.SetFlags(0);

            oPackage.SetValue(1, "optId", commonParams.optId);                  //柜员代码
            oPackage.SetValue(1, "optPwd", commonParams.optPwd);                //柜员密码
            oPackage.SetValue(1, "optMode", commonParams.optMode);              //委托方式

            oPackage.SetValue(1, "acctId", commonParams.acctId);                //资金帐号
            oPackage.SetValue(1, "tradePwd", commonParams.tradePwd);                //交易密码
            oPackage.SetValue(1, "permitMac", commonParams.permitMac);                //登录Mac地址
            //oPackage.SetValue(1, "terminalInfo", commonParams.terminalInfo);                //登录Mac地址
            oPackage.SetValue(1, "exchId", Tangle2RootNet.OrderbookID2exchId(orderbookID)); //市场代码
            oPackage.SetValue(1, "contractNum", orderID);                //合同号

            //permitPhone 电话委托的主叫号码   N
            //custid  客户代码 N
            //exteriorAcctId 外部帐号    N
            //actionType  操作类型 N   asynchronous--表示该撤单请求可以使用异步方式
            bool ret = oPackage.ExchangeMessage();

            var x = oPackage.GetValue(0, "successflg");
            var c = oPackage.GetValue(0, "errorcode");
            var y = oPackage.GetValue(0, "failinfo");
            Console.WriteLine(string.Format("Error {0}:{1}", oPackage.GetValue(0, "errorCode"), oPackage.GetValue(0, "failInfo")));

            Console.WriteLine(oPackage.GetValue(1, "completeNum"));     //成功撤单笔数
            Console.WriteLine(oPackage.GetValue(1, "ordersum"));     //委托总笔数
        }

        public List<Stock.Order> GetOpenStockOrders()
        {
            oPackage.ClearSendPackage();

            oPackage.SetFunctionCode("00807300");   //查询可撤单委托
            oPackage.SetFlags(0);

            oPackage.SetValue(1, "optId", commonParams.optId);    // 柜员代码
            oPackage.SetValue(1, "optPwd", commonParams.optPwd);  // 柜员口令 

            oPackage.SetValue(1, "optMode", commonParams.optMode);  //  委托方式    Y
                                                                    //oPackage.SetValue(1, "grantExchList   交易市场代码 N
                                                                    //oPackage.SetValue(1, "custid 客户代码    N
            oPackage.SetValue(1, "acctId", commonParams.acctId);  //    资金帐号 N   二选一输入
                                                                  //oPackage.SetValue(1, "regId   股东代码 N
            oPackage.SetValue(1, "tradePwd", commonParams.tradePwd);  //   交易密码                    Y
                                                                      //oPackage.SetValue(1, "contractNum     合同序号 N
                                                                      //oPackage.SetValue(1, "stkId 证券代码                    N
                                                                      //oPackage.SetValue(1, "orderType       委托类型 N
                                                                      //oPackage.SetValue(1, "maxOrderPrice 价格上限                    N
                                                                      //oPackage.SetValue(1, "minOrderPrice   价格下限 N
                                                                      //oPackage.SetValue(1, "maxOrderQty 数量上限    N
                                                                      //oPackage.SetValue(1, "minOrderQty 数量下限 N
            oPackage.SetValue(1, "maxRowNum", "500");         // 每次返回的最大记录数  Y 取值范围：1～500
            oPackage.SetValue(1, "packNum", "1");         // 查询序号    Y 首次查询时为1，查下一页时递加1

            bool ret = oPackage.ExchangeMessage();

            var x = oPackage.GetValue(0, "successflg");
            var c = oPackage.GetValue(0, "errorcode");
            var y = oPackage.GetValue(0, "failinfo");
            Console.WriteLine(string.Format("Error {0}:{1}", oPackage.GetValue(0, "errorCode"), oPackage.GetValue(0, "failInfo")));


            List<Stock.Order> orders = new List<Stock.Order>();

            //获取返回记录条数
            int iCnt = int.Parse(oPackage.GetValue(0, "recordCnt"));

            //逐条获取返回的结果
            for (int i = 1; i <= iCnt; i++)
            {
                Stock.Order order = new Stock.Order();

                order.OrderID = oPackage.GetValue(i, "contractNum"); // 合同序号
                order.OrderbookID = oPackage.GetValue(i, "stkId") + "." +
                    RootNet2Tangle.TransExchID(oPackage.GetValue(i, "exchId")); //证券代码+交易市场
                order.OrderbookName = oPackage.GetValue(i, "stkName");      //证券名称
                order.Side = RootNet2Tangle.TransOrderType(oPackage.GetValue(i, "orderType"));  //买卖类别
                order.OrderPrice = decimal.Parse(oPackage.GetValue(i, "orderPrice"));   //委托价格
                order.Quantity = int.Parse(oPackage.GetValue(i, "orderQty"));   //委托数量
                order.FilledQuantity = int.Parse(oPackage.GetValue(i, "knockQty"));   //成交数量,单位为委托单位
                order.CancelledQuantity = int.Parse(oPackage.GetValue(i, "withdrawQty"));   //撤单数量,单位为委托单位
                order.OrderTime = DateTime.Parse(oPackage.GetValue(i, "orderTime"));     //委托时间

                orders.Add(order);
                //sendFlag    报盘标志
                //regId 股东代码
                //regName 股东姓名
                //acctId  资金帐号
                //exteriorAcctId  外部帐号 特定用户使用
                //offerTime 申报时间
            }

            return orders;
        }

        public string AddFutureOrder(string orderbookID, int quantity, ORDER_SIDE side, POSITION_EFFECT effect, decimal price = -1)
        {
            oPackage.ClearSendPackage();

            oPackage.SetFunctionCode("20100030");   //期货普通报单
            oPackage.SetFlags(0);

            oPackage.SetValue(1, "optId", commonParams.optId);                  //柜员代码
            oPackage.SetValue(1, "optPwd", commonParams.optPwd);                //柜员密码
            oPackage.SetValue(1, "optMode", commonParams.optMode);              //委托方式
            oPackage.SetValue(1, "permitMac", commonParams.permitMac);          //登录Mac地址
            //oPackage.SetValue(1, "terminalInfo", commonParams.terminalInfo);                //终端信息

            oPackage.SetValue(1, "regId", commonParams.regId);                  // 股东代码 N


            //acctId 资金帐号    Y
            //acctPwd 资金密码 Y
            //exchId 市场代码    Y
            //regId   交易编码 Y
            //tradePwd 交易密码    Y
            //currencyId  货币代码 Y
            //stkId 合约编码    Y
            //F_offSetFlag    开平标志 Y(OPEN, CLOSE)
            //bsFlag 合约方向    Y B-多头 S - 空头
            //F_hedgeFlag 投机套保标记  N 中金所不需要送，上期所、郑商所、大商所需要送
            //orderQty    委托数量 Y
            //F_orderPriceType 报单价格条件  Y
            //futureOrderPrice    委托价格 Y
            //coveredFlag 备兑标签    N   1 - 备兑,0 - 非备兑
            //F_MatchCondition 订单有效时间类型    N GFD-当日有效、FOK - 即时成交否则撤销、IOC - 即时成交剩余撤销
            //ClientId 策略ID    N
            //orderSource 订单来源 N   "期权做市使用(501-期权做市询价应答双边，502-期权做市连续报价双边，503-期权做市连续报价单边，504-期权做市询价应答单边)
            //"
            //businessMark 业务类别    N OTO-个股期权交易,ORQ - 回应询价,ORR - 回应询价修改
            //origContractNum 原询价回应合同序号   N 当业务类型 = ORR时要求必送, 送被修改询价回应订单(ORQ)的合同序号,其他业务不送

            return null;

        }

        public void CancelFutureOrder(string orderID)
        {
            throw new NotImplementedException();
        }

        public List<Future.Order> GetOpenFutureOrders()
        {
            throw new NotImplementedException();
        }

        public void SynchronizeFutureAccount()
        {
            throw new NotImplementedException();
        }
    }
}
