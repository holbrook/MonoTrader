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
    public class RootNetBroker: IBroker, IStockBroker, IFutureBroker
    {
        private GWDPApiCS.GWDPApiCS oPackage;
        private dynamic commonParams = new ExpandoObject();

        public void Initialize(dynamic param)
        {
            commonParams.optId = "99990";       // 柜员代码
            commonParams.optPwd = "112233";     // 柜员口令
            commonParams.optMode = "W5";        // 委托方式
            commonParams.acctId = "001653019819";        //现货资金帐号
            commonParams.tradePwd = "135246";     // 现货资金密码
            commonParams.regId = "D890019819";   //股东代码

            //TODO: 查询股东账号, 考虑两个市场？

            oPackage = new GWDPApiCS.GWDPApiCS();

            //初始化。一个对象初始化一次就可以
            if (oPackage.Init() == false)
            { 
                //Logger.Error("初始化通讯对象失败！");
                return;
            }
        }

        private bool SendPackage(string funCode, List<RNField> fields, short flag=0)
        {
            //清空发送请求包
            oPackage.ClearSendPackage();
            //设置功能号
            oPackage.SetFunctionCode(funCode);
            //设置包头flag
            oPackage.SetFlags(flag);

            foreach(var field in fields)
            {
                oPackage.SetValue(field.Flag, field.FieldName, field.FieldValue);
            }

            return oPackage.ExchangeMessage();
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


        public void SynchronizeAccount()
        {
            //throw new NotImplementedException();
        }

        public string AddOrder(string orderbookID, int quantity, ORDER_SIDE side, decimal price)
        {
            ////设置功能号
            string funcCode = "00100030";                   //普通买卖委托

            List<RNField> req = new List<RNField>();
            req.Add(new RNField(1, "optId", commonParams.optId));                  //柜员代码
            req.Add(new RNField(1, "optPwd", commonParams.optPwd));                //柜员密码
            req.Add(new RNField(1, "optMode", commonParams.optMode));              //委托方式
            req.Add(new RNField(1, "regId", commonParams.regId));                  // 股东代码 N
            req.Add(new RNField(1, "tradePwd", commonParams.tradePwd));                   //交易密码 Y
            req.Add(new RNField(1, "exchId", Tangle2RootNet.OrderbookID2exchId(orderbookID))); //市场代码 Y
            req.Add(new RNField(1, "stkId", Tangle2RootNet.OrderbookID2stkId(orderbookID)));     //证券代码    Y 转股回售、权证行权时不使用这个字段
            req.Add(new RNField(1, "orderType", Tangle2RootNet.TransOrderType(side)));   //交易类型 Y   JB - 行权，KS - ETF赎回，IS - 场内开放式基金可赎回数量

            //委托价格    Y 市价委托时固定送0
            if (0m == price)
            {
                req.Add(new RNField(1, "orderPrice", "0"));
            }
            else
            {
                req.Add(new RNField(1, "orderPrice", price.ToString()));
            }

            req.Add(new RNField(1, "orderQty", quantity.ToString()));   //委托数量    Y

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
            //terminalInfo 终端信息    Y   "PC;IIP:公网IP; LIP:内网IP; MAC:MAC地址; HD:硬盘序列号; PCN:计算机名;CPU:CPU序列号; PI:硬盘分区信息（系统盘符,分区格式,硬盘大小）
            //示例：
            //PC; IIP: 123.112.54.8; LIP: 192.168.8.7; MAC: 00000000000000E0; HD: TF655AY91GHRVL; PCN: BJ - OA - ZHNAG - XC; CPU: BFEBFBFF000306A9; PI: C,NTFS,80"
            //req.Add(new RNField(0, "recordCnt", "1"));                  //设置请求记录数

            if (!SendPackage(funcCode, req,0))
                return null;

            //返回合同号
            return oPackage.GetValue(1, " contractNum");
        }

        public void CancelOrder(Stock.Order order)
        {
            throw new NotImplementedException();
        }

        public List<Stock.Order> GetOpenOrders()
        {
            string funcCode = "00807300";        //查询可撤单委托

            List<RNField> req = new List<RNField>();
            req.Add(new RNField(1, "optId", commonParams.optId));    // 柜员代码
            req.Add(new RNField(1, "optPwd", commonParams.optPwd));  // 柜员口令 

            req.Add(new RNField(1, "optMode", commonParams.optMode));  //  委托方式    Y
            //req.Add(new RNField(1, "grantExchList   交易市场代码 N
            //req.Add(new RNField(1, "custid 客户代码    N
            req.Add(new RNField(1, "acctId", commonParams.acctId));  //    资金帐号 N   二选一输入
            //req.Add(new RNField(1, "regId   股东代码 N
            req.Add(new RNField(1, "tradePwd", commonParams.tradePwd));  //   交易密码                    Y
            //req.Add(new RNField(1, "contractNum     合同序号 N
            //req.Add(new RNField(1, "stkId 证券代码                    N
            //req.Add(new RNField(1, "orderType       委托类型 N
            //req.Add(new RNField(1, "maxOrderPrice 价格上限                    N
            //req.Add(new RNField(1, "minOrderPrice   价格下限 N
            //req.Add(new RNField(1, "maxOrderQty 数量上限    N
            //req.Add(new RNField(1, "minOrderQty 数量下限 N
            req.Add(new RNField(1, "maxRowNum", "500"));         // 每次返回的最大记录数  Y 取值范围：1～500
            req.Add(new RNField(1, "packNum", "1"));         // 查询序号    Y 首次查询时为1，查下一页时递加1

            if (!SendPackage(funcCode, req))
                return null;

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

       




        Future.Order IFutureBroker.AddOrder(string orderbookID, int quantity, ORDER_SIDE side, POSITION_EFFECT effect, decimal price)
        {
            throw new NotImplementedException();
        }

        void IFutureBroker.CancelOrder(Future.Order order)
        {
            throw new NotImplementedException();
        }

        List<Future.Order> IFutureBroker.GetOpenOrders()
        {
            throw new NotImplementedException();
        }
    }
}
