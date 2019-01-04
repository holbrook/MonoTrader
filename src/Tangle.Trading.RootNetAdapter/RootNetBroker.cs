using System;
using System.Collections.Generic;
using Tangle.Plugin;

namespace Tangle.Trading.RootNetAdapter
{
    public class RootNetBroker:IPart
    {
        private GWDPApiCS.GWDPApiCS oPackage;

        public IPartContext Context { get; set; }

        public void Initialize(dynamic param)
        {
            oPackage = new GWDPApiCS.GWDPApiCS();

            //初始化。一个对象初始化一次就可以
            if (oPackage.Init() == false)
            { 
                Context.Logger.Error("初始化通讯对象失败！");
                return;
            }
        }

        private void SendPackage(string funCode, short flag, List<RNField> fields)
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

        //委托
        void Order()
        {

            //设置功能号
            string funcCode = "00100030";

            //设置包头flag
            short flag = 0;

            List<RNField> req = new List<RNField>();
            req.Add(new RNField(0, "recordCnt", "1"));                  //设置请求记录数
            req.Add(new RNField(1, "optId", "99990"));                  //设置柜员代码
            req.Add(new RNField(1, "optPwd", "666666"));                //设置柜员密码
            req.Add(new RNField(1, "optMode", "A1"));             //设置委托方式
            req.Add(new RNField(1, "exchId", "0"));               //设置市场代码
            req.Add(new RNField(1, "regId", "A012345678"));             //设置股东代码
            req.Add(new RNField(1, "tradePwd", "666666"));              //设置交易密码
            req.Add(new RNField(1, "stkId", "600030"));                 //设置证券代码
            req.Add(new RNField(1, "orderType", "B"));                  //设置买卖方向
            req.Add(new RNField(1, "orderQty", "100"));                 //设置委托数量
            req.Add(new RNField(1, "orderPrice", "23.26"));             //设置委托价格
            req.Add(new RNField(1, "permitMac","E3A4D7CBF6AF"));              //设置MAC地址

            SendPackage(funcCode, flag, req);
            //和网关交互
            if (oPackage.ExchangeMessage() == false)
            {
                MessageBox.Show("委托请求（00100211）交互失败");
                oPackage.UnInit();
                return;
            }

            //获取成功失败标志
            if (oPackage.GetValue(0, "successflag").Equals("0") == false)
            {//失败
                MessageBox.Show(string.Format("委托失败，错误代码：{0}，错误信息{1}", oPackage.GetValue(0, "errorcode"), oPackage.GetValue(0, "failinfo")));
                oPackage.UnInit();
                return;
            }

            //获取返回记录条数
            int iCnt = int.Parse(oPackage.GetValue(0, "recordCnt"));
            //获取返回的合同序号
            string sContractNum = oPackage.GetValue(1, " contractNum");


        }

        //查询委托
        void QueryOrder()
        {

            //清空发送请求包
            oPackage.ClearSendPackage();

            //设置功能号
            oPackage.SetFunctionCode("00800010");

            //设置包头flag
            oPackage.SetFlags(0);

            //设置请求记录数
            oPackage.SetValue(0, "recordCnt", "1");

            //设置柜员代码
            oPackage.SetValue(1, "optId", "99990");

            //设置柜员密码
            oPackage.SetValue(1, "optPwd", "666666");

            //设置委托方式
            oPackage.SetValue(1, "optMode", "A1");

            //设置市场代码
            oPackage.SetValue(1, "exchId", "0");

            //设置资金帐号
            oPackage.SetValue(1, "acctId", "000010500531");

            //设置交易密码
            oPackage.SetValue(1, "tradePwd", "666666");

            //设置明细汇总标志(0-明细/1-按合同号汇总  2-按资金帐号汇总)
            oPackage.SetValue(1, "queryType", "0");

            //查询类接口中，入参包含maxRowNum(最大记录条数)和packNum(查询序号)，每次查询最多只返回maxRowNum条记录，若要取得后续结果集中的数据，需要packNum+1，再次执行查询。
            //设置每次返回的最大记录条数
            oPackage.SetValue(1, "maxRowNum", "500");

            //设置查询序号
            oPackage.SetValue(1, "packNum", "1");

            //和网关交互
            if (oPackage.ExchangeMessage() == false)
            {
                MessageBox.Show("委托查询（00800010）交互失败");
                oPackage.Unit();
                return;
            }

            //获取成功失败标志
            if (oPackage.GetValue(0, "successflag").Equals("0") == false)
            {//失败
                MessageBox.Show(string.Format("查询委托失败，错误代码：{0}，错误信息{1}", oPackage.GetValue(0, "errorcode"), oPackage.GetValue(0, "failinfo")));
                oPackage.Unit();
                return;
            }

            //获取返回记录条数
            int iCnt = int.Parse(oPackage.GetValue(0, "recordCnt"));

            //逐条获取返回的结果
            for (int i = 1; i <= iCnt; i++)
            {
                //获取返回的合同序号
                string sContractNum = oPackage.GetValue(i, " contractNum");
                //获取股东代码
                string sRegId = oPackage.GetValue(i, " regId");

                //获取证券代码
                string sStkId = oPackage.GetValue(i, "stkId");

                //获取市场代码
                string sExchId = oPackage.GetValue(i, "exchId");

                //获取买卖方向
                string sOrderType = oPackage.GetValue(i, "orderType");

                //获取委托价格
                string sOrderPrice = oPackage.GetValue(i, "orderPrice");

                //获取委托数量
                string sOrderQty = oPackage.GetValue(i, "orderQty");

            }


        }



    }
}
