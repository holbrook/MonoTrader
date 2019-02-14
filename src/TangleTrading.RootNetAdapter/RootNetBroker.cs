using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Dynamic;
using TangleTrading.Adapter;
using TangleTrading.Base;
using TangleTrading.Future;
using TangleTrading.Stock;

namespace TangleTrading.RootNetAdapter
{
    [Export(typeof(IBroker))]
    [Export(typeof(IFeed))]

    [Broker("根网适配器",typeof(RootNetConfig))]    
    //[Feed(typeof(Stock.Tick), new string[] { "XSHE", "XSHG", "NEEQ" })]
    //[Feed(typeof(Future.Tick), new string[] { "XSHE", "XSHG", "NEEQ" })]

    public class RootNetBroker : IBroker, IStockBroker, IFutureBroker
    {
        private GWDPApiCS.GWDPApiCS oPackage;
        //private dynamic commonParams = new ExpandoObject();
        private string permitMac = null;

        private RootNetConfig config = null;

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
            config = param as RootNetConfig;

            if (null == config)
            {
                //TODO: Log
                return;
            }          


            permitMac = info.MacAddress;

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


        public ExecuteStatus SynchronizeStockAccount()
        {
            ExecuteStatus s1 = _synchronizeStockAccount();
            if (s1.Code != 0)
                return s1;

            ExecuteStatus s2 = _synchronizeStockPosition();

            if (s2.Code != 0)
                return s2;

            return new ExecuteStatus(0, "同步股票账户成功");

        }

        private ExecuteStatus _synchronizeStockAccount()
        {
            oPackage.ClearSendPackage();

            oPackage.SetFunctionCode("00800140");   //资金查询
            oPackage.SetFlags(0);

            oPackage.SetValue(1, "optId", config.OptID);                  //柜员代码
            oPackage.SetValue(1, "optPwd", config.OptPwd);                //柜员密码
            oPackage.SetValue(1, "optMode", config.OptMode);              //委托方式
            //oPackage.SetValue(1, "permitMac", commonParams.permitMac);          //登录Mac地址
            //oPackage.SetValue(1, "terminalInfo", commonParams.terminalInfo);                //终端信息

            oPackage.SetValue(1, "acctId", config.Brokers["IStockBroker"].AccountID);                  // 资金账号
            oPackage.SetValue(1, "tradePwd", config.Brokers["IStockBroker"].AccountPwd);                   //交易密码 Y

            oPackage.SetValue(1, "maxRowNum", "500");                   //每次返回的最大记录数  Y 取值范围：1～500
            oPackage.SetValue(1, "packNum", "1");                   //查询序号    Y 首次查询时为1，查下一页时递加1

            //custid  客户代码 N
            //currencyId 货币代码    Y
            //regId   股东代码 N  二选一输入
            //flag    参考市值中体现当日逆回购数据标志 N   1 - 体现当日逆回购数据；其他值 - 不体现当日逆回购数据


            bool flag = oPackage.ExchangeMessage();
            if (!flag)
                return new ExecuteStatus(-1, "调用失败，请检查日志");

            ExecuteStatus ret = new ExecuteStatus();
            ret.Code = int.Parse(oPackage.GetValue(0, "successflg"));

            if(0!= ret.Code)
                ret.Message = oPackage.GetValue(0, "errorcode")+":"+ oPackage.GetValue(0, "failinfo");

            else
            {
                ret.Message = "调用成功";

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

                ret.Data = StockAccountInfo;
            }

            return ret;
        }


        private ExecuteStatus _synchronizeStockPosition()
        {
            oPackage.ClearSendPackage();

            oPackage.SetFunctionCode("00300021");   //股份查询（建议交易查询时使用，效率高） 
            oPackage.SetFlags(0);

            oPackage.SetValue(1, "optId", config.OptID);                  //柜员代码
            oPackage.SetValue(1, "optPwd", config.OptPwd);                //柜员密码
            oPackage.SetValue(1, "optMode", config.OptMode);              //委托方式
            oPackage.SetValue(1, "permitMac", permitMac);          //登录Mac地址
            //oPackage.SetValue(1, "terminalInfo", commonParams.terminalInfo);                //终端信息

            oPackage.SetValue(1, "acctId", config.Brokers["IStockBroker"].AccountID);                  // 资金账号
            oPackage.SetValue(1, "tradePwd", config.Brokers["IStockBroker"].AccountPwd);                   //交易密码 Y


            //custid  客户代码 N
            //exchId 交易市场代码  N 送股东代码的必须送市场
            //regId 股东代码                    N
            //stkId 证券代码    N
            //stkType 证券类别 N


            bool flag = oPackage.ExchangeMessage();
            if (!flag)
                return new ExecuteStatus(-1, "调用失败，请检查日志");

            ExecuteStatus ret = new ExecuteStatus();
            ret.Code = int.Parse(oPackage.GetValue(0, "successflg"));

            if (0 != ret.Code)
                ret.Message = oPackage.GetValue(0, "errorcode") + ":" + oPackage.GetValue(0, "failinfo");

            else
            {
                ret.Message = "调用成功";

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

                ret.Data = StockAccountInfo;
            }

            return ret;
        }



        public ExecuteStatus AddStockOrder(string orderbookID, int quantity, ORDER_SIDE side, decimal price)
        {
            //TODO: 市价委托?
            oPackage.ClearSendPackage();

            oPackage.SetFunctionCode("00100030");   //普通买卖委托
            oPackage.SetFlags(0);

            oPackage.SetValue(1, "optId", config.OptID);                  //柜员代码
            oPackage.SetValue(1, "optPwd", config.OptPwd);                //柜员密码
            oPackage.SetValue(1, "optMode", config.OptMode);              //委托方式
            oPackage.SetValue(1, "permitMac", permitMac);          //登录Mac地址
            //oPackage.SetValue(1, "terminalInfo", commonParams.terminalInfo);                //终端信息

            oPackage.SetValue(1, "acctId", config.Brokers["IStockBroker"].AccountID);                  // 资金账号
            oPackage.SetValue(1, "tradePwd", config.Brokers["IStockBroker"].AccountPwd);                   //交易密码 Y



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

            bool flag = oPackage.ExchangeMessage();
            if (!flag)
                return new ExecuteStatus(-1, "调用失败，请检查日志");

            ExecuteStatus ret = new ExecuteStatus();
            ret.Code = int.Parse(oPackage.GetValue(0, "successflg"));

            if (0 != ret.Code)
                ret.Message = oPackage.GetValue(0, "errorcode") + ":" + oPackage.GetValue(0, "failinfo");

            else
            {
                ret.Message = "调用成功";

                //返回合同号
                ret.Data = oPackage.GetValue(1, "contractNum");
            }

            return ret;
        }

        public ExecuteStatus CancelStockOrder(string orderID)
        {
            string orderbookID = null;

            List<Stock.Order> orders = GetOpenStockOrders().Data;
            foreach (var order in orders)
            {
                if (order.OrderID == orderID)
                {
                    orderbookID = order.OrderbookID;
                    break;
                }
            }

            if (null == orderbookID)
                return new ExecuteStatus(-1, "未找到可撤的订单:" + orderID);

            oPackage.ClearSendPackage();

            oPackage.SetFunctionCode("00100110");   //批量撤单委托
            oPackage.SetFlags(0);






            //文档中没写，但是要送！！！

            oPackage.SetValue(0, "recordCnt", "1");
            
            
            //oPackage.SetValue(1, "maxRowNum", "500");
            //oPackage.SetValue(1, "packNum", "1");
            //oPackage.SetValue(1, "actionType", "asynchronous");

            //======================================================

            oPackage.SetValue(1, "optId", config.OptID);                  //柜员代码
            oPackage.SetValue(1, "optPwd", config.OptPwd);                //柜员密码
            oPackage.SetValue(1, "optMode", config.OptMode);              //委托方式
            oPackage.SetValue(1, "permitMac", permitMac);          //登录Mac地址
            //oPackage.SetValue(1, "terminalInfo", commonParams.terminalInfo);                //终端信息

            oPackage.SetValue(1, "acctId", config.Brokers["IStockBroker"].AccountID);                  // 资金账号
            oPackage.SetValue(1, "tradePwd", config.Brokers["IStockBroker"].AccountPwd);                   //交易密码 Y


            
            oPackage.SetValue(1, "exchId", Tangle2RootNet.OrderbookID2exchId(orderbookID)); //市场代码
            oPackage.SetValue(1, "contractNum", orderID);                //合同号

            //permitPhone 电话委托的主叫号码   N
            //custid  客户代码 N
            //exteriorAcctId 外部帐号    N
            //actionType  操作类型 N   asynchronous--表示该撤单请求可以使用异步方式


            bool flag = oPackage.ExchangeMessage();
            if (!flag)
                return new ExecuteStatus(-1, "调用失败，请检查日志");

            ExecuteStatus ret = new ExecuteStatus();
            ret.Code = int.Parse(oPackage.GetValue(0, "successflg"));

            if (0 != ret.Code)
                ret.Message = oPackage.GetValue(0, "errorcode") + ":" + oPackage.GetValue(0, "failinfo");

            else
            {
                ret.Message = "调用成功";
                //ret.Data =
                //Console.WriteLine(oPackage.GetValue(1, "completeNum"));     //成功撤单笔数
                //Console.WriteLine(oPackage.GetValue(1, "ordersum"));     //委托总笔数
            }

            return ret;
        }

        public ExecuteStatus GetOpenStockOrders()
        {
            oPackage.ClearSendPackage();

            oPackage.SetFunctionCode("00807300");   //查询可撤单委托
            oPackage.SetFlags(0);

            oPackage.SetValue(1, "optId", config.OptID);                  //柜员代码
            oPackage.SetValue(1, "optPwd", config.OptPwd);                //柜员密码
            oPackage.SetValue(1, "optMode", config.OptMode);              //委托方式
            oPackage.SetValue(1, "permitMac", permitMac);          //登录Mac地址
            //oPackage.SetValue(1, "terminalInfo", commonParams.terminalInfo);                //终端信息

            oPackage.SetValue(1, "acctId", config.Brokers["IStockBroker"].AccountID);                  // 资金账号
            oPackage.SetValue(1, "tradePwd", config.Brokers["IStockBroker"].AccountPwd);                   //交易密码 Y


          //oPackage.SetValue(1, "regId   股东代码 N
          //oPackage.SetValue(1, "grantExchList   交易市场代码 N
          //oPackage.SetValue(1, "custid 客户代码    N

            //oPackage.SetValue(1, "contractNum     合同序号 N
            //oPackage.SetValue(1, "stkId 证券代码                    N
            //oPackage.SetValue(1, "orderType       委托类型 N
            //oPackage.SetValue(1, "maxOrderPrice 价格上限                    N
            //oPackage.SetValue(1, "minOrderPrice   价格下限 N
            //oPackage.SetValue(1, "maxOrderQty 数量上限    N
            //oPackage.SetValue(1, "minOrderQty 数量下限 N
            oPackage.SetValue(1, "maxRowNum", "500");         // 每次返回的最大记录数  Y 取值范围：1～500
            oPackage.SetValue(1, "packNum", "1");         // 查询序号    Y 首次查询时为1，查下一页时递加1

            bool flag = oPackage.ExchangeMessage();
            if (!flag)
                return new ExecuteStatus(-1, "调用失败，请检查日志");

            ExecuteStatus ret = new ExecuteStatus();
            ret.Code = int.Parse(oPackage.GetValue(0, "successflg"));
            ret.Data = new List<Stock.Order>();

            if (0 != ret.Code)
                ret.Message = oPackage.GetValue(0, "errorcode") + ":" + oPackage.GetValue(0, "failinfo");

            else
            {
                ret.Message = "调用成功";
                List<Stock.Order> orders = ret.Data;

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
                ret.Data = orders;
            }
            
            return ret;
        }

        public ExecuteStatus AddFutureOrder(string orderbookID, int quantity, ORDER_SIDE side, POSITION_EFFECT effect, decimal price = 0m)
        {
            oPackage.ClearSendPackage();

            oPackage.SetFunctionCode("20100030");   //期货普通报单
            oPackage.SetFlags(0);

            oPackage.SetValue(1, "optId", config.OptID);                  //柜员代码
            oPackage.SetValue(1, "optPwd", config.OptPwd);                //柜员密码
            oPackage.SetValue(1, "optMode", config.OptMode);              //委托方式
            oPackage.SetValue(1, "permitMac", permitMac);          //登录Mac地址
            //oPackage.SetValue(1, "terminalInfo", commonParams.terminalInfo);                //终端信息

            oPackage.SetValue(1, "acctId", config.Brokers["IStockBroker"].AccountID);                  // 资金账号
            oPackage.SetValue(1, "tradePwd", config.Brokers["IStockBroker"].AccountPwd);                   //交易密码 Y



            oPackage.SetValue(1, "exchId", Tangle2RootNet.OrderbookID2exchId(orderbookID)); //市场代码 Y
            oPackage.SetValue(1, "regId", config.Brokers["IFutureBroker"].Accounts[orderbookID.Split('.')[1]].RegID);                  // 股东代码 N
            oPackage.SetValue(1, "tradePwd", config.Brokers["IFutureBroker"].Accounts[orderbookID.Split('.')[1]].TradePwd);            //交易密码 Y

            oPackage.SetValue(1, "currencyId", "00");//   人民币     //currencyId  货币代码 Y

            oPackage.SetValue(1, "stkId", Tangle2RootNet.OrderbookID2stkId(orderbookID));     //合约编码 

            oPackage.SetValue(1, "F_offSetFlag", Tangle2RootNet.TransF_offSetFlag(effect));   //    开平标志 Y(OPEN, CLOSE)

            oPackage.SetValue(1, "bsFlag", Tangle2RootNet.TransOrderType(side));  //bsFlag 合约方向    Y B-多头 S - 空头
            oPackage.SetValue(1, "orderQty", quantity.ToString());  //orderQty    委托数量 Y

            oPackage.SetValue(1, "F_orderPriceType", (0m == price) ? "ANY" : "LIMIT"); // 报单价格条件  Y
            oPackage.SetValue(1, "futureOrderPrice", price.ToString());  //orderQty    委托数量 Y

            //F_hedgeFlag 投机套保标记  N 中金所不需要送，上期所、郑商所、大商所需要送
            //coveredFlag 备兑标签    N   1 - 备兑,0 - 非备兑
            //F_MatchCondition 订单有效时间类型    N GFD-当日有效、FOK - 即时成交否则撤销、IOC - 即时成交剩余撤销
            //ClientId 策略ID    N
            //orderSource 订单来源 N   "期权做市使用(501-期权做市询价应答双边，502-期权做市连续报价双边，503-期权做市连续报价单边，504-期权做市询价应答单边)
            //"
            //businessMark 业务类别    N OTO-个股期权交易,ORQ - 回应询价,ORR - 回应询价修改
            //origContractNum 原询价回应合同序号   N 当业务类型 = ORR时要求必送, 送被修改询价回应订单(ORQ)的合同序号,其他业务不送

            bool flag = oPackage.ExchangeMessage();
            if (!flag)
                return new ExecuteStatus(-1, "调用失败，请检查日志");

            ExecuteStatus ret = new ExecuteStatus();
            ret.Code = int.Parse(oPackage.GetValue(0, "successflg"));

            if (0 != ret.Code)
                ret.Message = oPackage.GetValue(0, "errorcode") + ":" + oPackage.GetValue(0, "failinfo");

            else
            {
                ret.Message = "调用成功";
                ret.Data = oPackage.GetValue(1, "contractNum");  // 合同序号
                //orderTime 委托时间
                //orderQty 委托数量
                //openFrozMargin 开仓冻结保证金
                //usableAmt 可用金额
                //serialnum 流水号
            }

            return ret;
        }

        public ExecuteStatus CancelFutureOrder(string orderID)
        {
            string orderbookID = null;

            List<Future.Order> orders = GetOpenFutureOrders().Data;
            foreach (var order in orders)
            {
                if (order.OrderID == orderID)
                {
                    orderbookID = order.OrderbookID;
                    break;
                }
            }

            if (null == orderbookID)
                return new ExecuteStatus(-1, "未找到可撤的订单:" + orderID);

            oPackage.ClearSendPackage();

            oPackage.SetFunctionCode("20100031");   //期货报单操作请求
            oPackage.SetFlags(0);

            oPackage.SetValue(1, "optId", config.OptID);                  //柜员代码
            oPackage.SetValue(1, "optPwd", config.OptPwd);                //柜员密码
            oPackage.SetValue(1, "optMode", config.OptMode);              //委托方式
            oPackage.SetValue(1, "permitMac", permitMac);          //登录Mac地址
            //oPackage.SetValue(1, "terminalInfo", commonParams.terminalInfo);                //终端信息

            oPackage.SetValue(1, "acctId", config.Brokers["IStockBroker"].AccountID);                  // 资金账号
            oPackage.SetValue(1, "acctPwd", config.Brokers["IStockBroker"].AccountPwd);                   //交易密码 Y



            oPackage.SetValue(1, "contractNum", orderID);          //合同序号


            oPackage.SetValue(1, "exchId", Tangle2RootNet.OrderbookID2exchId(orderbookID)); //市场代码 Y
            oPackage.SetValue(1, "regId", config.Brokers["IStockBroker"].Accounts[orderbookID.Split('.')[1]].RegID);                  // 股东代码 N
            oPackage.SetValue(1, "tradePwd", config.Brokers["IStockBroker"].Accounts[orderbookID.Split('.')[1]].TradePwd);            //交易密码 Y

            oPackage.SetValue(1, "actionFlag", "DELETE");   //      报单操作类型 Y


            bool flag = oPackage.ExchangeMessage();
            if (!flag)
                return new ExecuteStatus(-1, "调用失败，请检查日志");

            ExecuteStatus ret = new ExecuteStatus();
            ret.Code = int.Parse(oPackage.GetValue(0, "successflg"));

            if (0 != ret.Code)
                ret.Message = oPackage.GetValue(0, "errorcode") + ":" + oPackage.GetValue(0, "failinfo");

            else
            {
                ret.Message = "期货撤单成功";
                //输出参数 参数名 说明 备注
                //serialNum 流水号

            }
            return ret;
        }

        public ExecuteStatus GetOpenFutureOrders()
        {

            oPackage.ClearSendPackage();

            oPackage.SetFunctionCode("20800010");   //当日报单情况查询
            oPackage.SetFlags(0);

            oPackage.SetValue(1, "optId", config.OptID);                  //柜员代码
            oPackage.SetValue(1, "optPwd", config.OptPwd);                //柜员密码
            oPackage.SetValue(1, "optMode", config.OptMode);              //委托方式
            oPackage.SetValue(1, "permitMac", permitMac);          //登录Mac地址
            //oPackage.SetValue(1, "terminalInfo", commonParams.terminalInfo);                //终端信息

            oPackage.SetValue(1, "acctId", config.Brokers["IFutureBroker"].AccountID);                  // 资金账号
            oPackage.SetValue(1, "acctPwd", config.Brokers["IFutureBroker"].AccountPwd);                   //交易密码 Y


            //oPackage.SetValue(1, "exchId", Tangle2RootNet.OrderbookID2exchId(orderbookID)); //市场代码 N
            //oPackage.SetValue(1, "regId", commonParams.accounts[""].regId);                  // 股东代码 N
            //oPackage.SetValue(1, "tradePwd", commonParams.accounts[""].pwd);
            //F_offSetFlag    开平标志 N
            //  F_orderStatus 报单状态    N
            //contractNum 合同序号 N
            //ClientId 策略ID    N
            //stkId   合约代码 N

            bool flag = oPackage.ExchangeMessage();
            if (!flag)
                return new ExecuteStatus(-1, "调用失败，请检查日志");

            ExecuteStatus ret = new ExecuteStatus();
            ret.Code = int.Parse(oPackage.GetValue(0, "successflg"));

            if (0 != ret.Code)
                ret.Message = oPackage.GetValue(0, "errorcode") + ":" + oPackage.GetValue(0, "failinfo");

            else
            {
                ret.Message = "期货查询可撤委托成功";

                List<Future.Order> orders = new List<Future.Order>();

                //获取返回记录条数
                int iCnt = int.Parse(oPackage.GetValue(0, "recordCnt"));

                //F_orderStatus 报单状态
                //NEW 已接收
                //ALLTRD 全部成交
                //P_TRD_Q 部分成交，还在队列中
                //P_TRD_NQ    部分成交，不在队列中
                //N_TRD_Q     未成交还在队列中
                //N_TRD_NQ    未成交不在队列中
                //CANCEL      撤单
                //DELETE_N    删除订单 - 新状态
                //DELETE_S_I 删除订单-内部撤单成功
                //DELETE_S_E 删除订单-交易所撤单成功
                //DELETE_F 删除订单-失败状态

                List<string> cancelableStatus = new List<string>(new string[]{
                    "NEW",
                    "P_TRD_Q", "P_TRD_NQ",
                    "N_TRD_Q", "N_TRD_NQ"
                 });


                //逐条获取返回的结果
                for (int i = 1; i <= iCnt; i++)
                {
                    if (!cancelableStatus.Contains(oPackage.GetValue(i, "F_orderStatus")))
                        continue;

                    Future.Order order = new Future.Order();

                    order.OrderID = oPackage.GetValue(i, "contractNum"); // 合同序号
                    order.OrderbookID = oPackage.GetValue(i, "stkId") + "." +
                        RootNet2Tangle.TransExchID(oPackage.GetValue(i, "exchId")); //证券代码+交易市场
                    order.OrderbookName = oPackage.GetValue(i, "stkName");      //证券名称
                    order.Side = RootNet2Tangle.TransOrderType(oPackage.GetValue(i, "bsFlag"));  //合约方向，       B－多头，S - 空头
                    order.OrderPrice = decimal.Parse(oPackage.GetValue(i, "futureOrderPrice"));   // 委托价格(精确到小数点后4位)
                    order.Quantity = int.Parse(oPackage.GetValue(i, "orderQty"));   //委托数量
                    order.FilledQuantity = int.Parse(oPackage.GetValue(i, "knockQty"));   //成交数量,单位为委托单位
                    order.CancelledQuantity = int.Parse(oPackage.GetValue(i, "withdrawQty"));   //撤单数量,单位为委托单位
                    order.OrderTime = DateTime.Parse(oPackage.GetValue(i, "orderTime"));     //委托时间

                    order.FilledPrice = decimal.Parse(oPackage.GetValue(i, "averagePrice"));   // 成交均价
                    order.PositonEffect = RootNet2Tangle.TransPositionEffect(oPackage.GetValue(i, "F_offSetFlag")); //开平标志


                    //F_orderStatus 报单状态
                    //F_forceCloseReason 强平原因
                    //exchErrorCode 交易所返回的错误代码
                    //exchErrorMsg 错误信息

                    orders.Add(order);

                    //actionFlag 报单的操作类型
                    //knockAmt 成交金额
                    //F_orderPriceType 报单价格条件
                    //acctId 资金帐户
                    //regName 客户姓名
                    //regId 交易编号


                    //validFlag 合法标志
                    //CoveredFlag 备兑标签        1 - 备兑,0 - 非备兑
                    //F_MatchCondition 订单有效时间类型        GFD - 当日有效、FOK - 即时成交否则撤销、IOC - 即时成交剩余撤销

                    //    ClientId 策略ID
                    //    orderId 交易的订单编号
                    //    serialNum 流水号
                    //basketid 组合代码        CTS_6.0.2.0后才支持
                }
                ret.Data = orders;


            }
            return ret;
        }

        public ExecuteStatus SynchronizeFutureAccount()
        {
            ExecuteStatus s1 = _syncFutureAccount();
            if (s1.Code != 0)
                return s1;

            ExecuteStatus s2 = _syncFuturePositions();

            if (s2.Code != 0)
                return s2;

            return new ExecuteStatus(0, "同步期货账户成功");
        }



        private ExecuteStatus _syncFutureAccount()
        {
            oPackage.ClearSendPackage();

            oPackage.SetFunctionCode("20800110");   //资金查询
            oPackage.SetFlags(0);

            oPackage.SetValue(1, "optId", config.OptID);                  //柜员代码
            oPackage.SetValue(1, "optPwd", config.OptPwd);                //柜员密码
            oPackage.SetValue(1, "optMode", config.OptMode);              //委托方式
            //oPackage.SetValue(1, "permitMac", permitMac);          //登录Mac地址
            //oPackage.SetValue(1, "terminalInfo", commonParams.terminalInfo);                //终端信息

            oPackage.SetValue(1, "acctId", config.Brokers["IFutureBroker"].AccountID);                  // 资金账号
            oPackage.SetValue(1, "acctPwd", config.Brokers["IFutureBroker"].AccountPwd);                   //交易密码 Y

            oPackage.SetValue(1, "maxRowNum", "500");                   //每次返回的最大记录数  Y 取值范围：1～500
            oPackage.SetValue(1, "packNum", "1");                   //查询序号    Y 首次查询时为1，查下一页时递加1

            //custid  客户代码 N
            //currencyId 货币代码    Y
            //regId   股东代码 N  二选一输入
            //flag    参考市值中体现当日逆回购数据标志 N   1 - 体现当日逆回购数据；其他值 - 不体现当日逆回购数据


            bool flag = oPackage.ExchangeMessage();
            if (!flag)
                return new ExecuteStatus(-1, "调用失败，请检查日志");

            ExecuteStatus ret = new ExecuteStatus();
            ret.Code = int.Parse(oPackage.GetValue(0, "successflg"));

            if (0 != ret.Code)
                ret.Message = oPackage.GetValue(0, "errorcode") + ":" + oPackage.GetValue(0, "failinfo");

            else
            {
                ret.Message = "调用成功";

                //获取返回记录条数
                int iCnt = int.Parse(oPackage.GetValue(0, "recordCnt"));

                FutureAccountInfo.Cash = decimal.Parse(oPackage.GetValue(1, "usableAmt"));   //可用金额
                FutureAccountInfo.FrozenCash =
                    decimal.Parse(oPackage.GetValue(1, "tradeFrozenAmt"));   //交易冻结

                FutureAccountInfo.MarketValue = decimal.Parse(oPackage.GetValue(1, "realtimeAmt"));   //权益（实时计算）  

                FutureAccountInfo.TransactionCost = decimal.Parse(oPackage.GetValue(1, "commision")); // 手续费用

                //custType 客户类别
                //currentAmt 当前余额

                //closePNL 平仓盈亏
                //RealtimePNL 实时盈亏（实时计算）              
                //YdMarginUsedAmt 昨日保证金占用
                //marginUsedAmt 当日保证金占用（实时计算）       
                //cashMovementAmt 当日出入金

                //overdraftLimit 信用资金

                ret.Data = FutureAccountInfo;
            }

            return ret;


        }

        private ExecuteStatus _syncFuturePositions()
        {
            oPackage.ClearSendPackage();

            oPackage.SetFunctionCode("20800100");   //查询客户期货持仓信息
            oPackage.SetFlags(0);

            oPackage.SetValue(1, "optId", config.OptID);                  //柜员代码
            oPackage.SetValue(1, "optPwd", config.OptPwd);                //柜员密码
            oPackage.SetValue(1, "optMode", config.OptMode);              //委托方式
            oPackage.SetValue(1, "permitMac", permitMac);          //登录Mac地址
            //oPackage.SetValue(1, "terminalInfo", commonParams.terminalInfo);                //终端信息

            oPackage.SetValue(1, "acctId", config.Brokers["IFutureBroker"].AccountID);                  // 资金账号
            oPackage.SetValue(1, "acctPwd", config.Brokers["IFutureBroker"].AccountPwd);                   //交易密码 Y


            oPackage.SetValue(1, "exchId", "CCFX");

            // 市场代码    Y


            bool flag = oPackage.ExchangeMessage();
            if (!flag)
                return new ExecuteStatus(-1, "调用失败，请检查日志");

            ExecuteStatus ret = new ExecuteStatus();
            ret.Code = int.Parse(oPackage.GetValue(0, "successflg"));

            if (0 != ret.Code)
                ret.Message = oPackage.GetValue(0, "errorcode") + ":" + oPackage.GetValue(0, "failinfo");

            else
            {
                ret.Message = "调用成功";

                FutureAccountInfo.Positions.Clear();

                //获取返回记录条数
                int iCnt = int.Parse(oPackage.GetValue(0, "recordCnt"));


                //逐条获取返回的结果
                for (int i = 1; i <= iCnt; i++)
                {
                    string orderbookID = oPackage.GetValue(i, "stkId") + "." +
                    RootNet2Tangle.TransExchID(oPackage.GetValue(i, "exchId"));

                    Future.Position position = new Future.Position();

                    position.OrderbookID = orderbookID;
                    position.OrderbookName = oPackage.GetValue(i, "stkName");
                    position.Quantity = int.Parse(oPackage.GetValue(i, "realTimePositionQty"));//  实时持仓数（实时计算）  
                    position.MarketValue = decimal.Parse(oPackage.GetValue(i, "newPrice"))* position.Quantity;// 证券市值
                    //position.AvgPrice = decimal.Parse(oPackage.GetValue(i, "previousCost"));// 参考成本
                    position.Profit = decimal.Parse(oPackage.GetValue(i, "closePNL"));// 参考收益
                    //position.TodayQuantity = position.Quantity -
                    //    int.Parse(oPackage.GetValue(i, "usableQty"));// 今仓 = 持仓-可用

                    //参数名 说明  备注

                    //acctId                      资金帐户
                    //regName                     帐户姓名
                    //regId                       交易编码


                    //bsFlag                      合约方向 B－多头，S - 空头
                    //currentPositionQty 当前持仓数量
                    //        
                    //YdPositionUsableQty 昨日持仓可平仓数
                    //todayPositionUsableQty 今日持仓可平仓数
                    //todayPositionCost 今开持仓均价
                    //preSettlementPrice 昨日结算价
                    //newPrice 最新价
                    //closePNL 平仓盈亏
                    //RealtimePNL 实时盈亏（实时计算）              
                    //OpenFrozPositionQty 开仓冻结数量
                    //TodayOffsFrozPositionQty 平今冻结
                    //YdOffsFrozPositionQty 平昨冻结数
                    //marginFrozenAmt 保证金冻结金额
                    //marginUsedAmt 当日保证金占用（实时计算）       
                    //TodayContractAmt 今日合约金额
                    //YdContractAmt 昨日持仓合约金额

                    FutureAccountInfo.Positions.Add(orderbookID, position);
                }

                ret.Data = FutureAccountInfo;
            }

            return ret;
        }


    }
}
