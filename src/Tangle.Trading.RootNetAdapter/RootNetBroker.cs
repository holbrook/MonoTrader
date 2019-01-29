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

        public Stock.Order AddOrder(string orderbookID, int quantity, ORDER_SIDE side, decimal price)
        {
            ////设置功能号
            //string funcCode = "00100030";

            ////设置包头flag
            //short flag = 0;

            //List<RNField> req = new List<RNField>();
            //req.Add(new RNField(0, "recordCnt", "1"));                  //设置请求记录数
            //req.Add(new RNField(1, "optId", "99990"));                  //设置柜员代码
            //req.Add(new RNField(1, "optPwd", "666666"));                //设置柜员密码
            //req.Add(new RNField(1, "optMode", "A1"));             //设置委托方式
            //req.Add(new RNField(1, "exchId", "0"));               //设置市场代码
            //req.Add(new RNField(1, "regId", "A012345678"));             //设置股东代码
            //req.Add(new RNField(1, "tradePwd", "666666"));              //设置交易密码
            //req.Add(new RNField(1, "stkId", "600030"));                 //设置证券代码
            //req.Add(new RNField(1, "orderType", "B"));                  //设置买卖方向
            //req.Add(new RNField(1, "orderQty", "100"));                 //设置委托数量
            //req.Add(new RNField(1, "orderPrice", "23.26"));             //设置委托价格
            //req.Add(new RNField(1, "permitMac", "E3A4D7CBF6AF"));              //设置MAC地址

            //SendPackage(funcCode, flag, req);
            ////和网关交互
            //if (oPackage.ExchangeMessage() == false)
            //{
            //    MessageBox.Show("委托请求（00100211）交互失败");
            //    oPackage.UnInit();
            //    return;
            //}

            ////获取成功失败标志
            //if (oPackage.GetValue(0, "successflag").Equals("0") == false)
            //{//失败
            //    MessageBox.Show(string.Format("委托失败，错误代码：{0}，错误信息{1}", oPackage.GetValue(0, "errorcode"), oPackage.GetValue(0, "failinfo")));
            //    oPackage.UnInit();
            //    return;
            //}

            ////获取返回记录条数
            //int iCnt = int.Parse(oPackage.GetValue(0, "recordCnt"));
            ////获取返回的合同序号
            //string sContractNum = oPackage.GetValue(1, " contractNum");
            return null;
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

        void IStockBroker.SynchronizeAccount()
        {
            throw new NotImplementedException();
        }


        void IFutureBroker.SynchronizeAccount()
        {
            throw new NotImplementedException();
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
