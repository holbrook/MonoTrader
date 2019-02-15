using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TangleTrading.RootNetAdapter
{
    public abstract class RootNetBase
    {
        protected GWDPApiCS.GWDPApiCS oPackage;
        protected dynamic commonParams = new ExpandoObject();

        public RootNetBase()
        { }

        public void Initialize(dynamic param)
        {

            RootNetConfig config = param as RootNetConfig;

            if (null == config)
            {
                //TODO: Log
                return;
            }

            commonParams.optId = config.OptID;
            commonParams.optPwd = config.OptPwd;
            commonParams.optMode = config.OptMode;

            commonParams.marketAccounts = new Dictionary<string, dynamic>();
            foreach (var broker in config.Brokers)
            {
                if (broker.BrokerType.ToLower().Contains("stock"))
                {
                    //TODO:验证股票资金帐号
                    commonParams.stockAccount = broker;
                }

                if (broker.BrokerType.ToLower().Contains("future"))
                {
                    //TODO:验证股票资金帐号
                    commonParams.futureAccount = broker;
                }

                foreach (var reg in broker.Accounts)
                {
                    commonParams.marketAccounts.Add(reg.MarketID, new ExpandoObject());
                    commonParams.marketAccounts[reg.MarketID].acctId = broker.AccountID;
                    commonParams.marketAccounts[reg.MarketID].acctPwd = broker.AccountPwd;
                    commonParams.marketAccounts[reg.MarketID].regId = reg.RegID;
                    commonParams.marketAccounts[reg.MarketID].regPwd = reg.TradePwd;
                }

            }

            HardwareInfo info = new HardwareInfo();
            commonParams.permitMac = info.MacAddress;

            //TODO: 同步一次账号

            oPackage = new GWDPApiCS.GWDPApiCS();

            //初始化。一个对象初始化一次就可以
            if (oPackage.Init() == false)
            {
                //Logger.Error("初始化通讯对象失败！");
                return;
            }
        }
    }
}
