using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TangleTrading.RootNetAdapter
{
    [XmlType]
    public class AccountConfig
    {
        /// <summary>
        /// 市场代码
        /// </summary>
        [XmlAttribute]
        public string MarketID { get; set; }

        /// <summary>
        /// 登记号。对于股票，就是股东帐号
        /// </summary>
        [XmlAttribute]
        public string RegID { get; set; }

        /// <summary>
        /// 交易密码
        /// </summary>
        [XmlAttribute]
        public string TradePwd { get; set; }

        public AccountConfig() { }

        public AccountConfig(string market,string regID, string pwd)
        {
            MarketID = market;
            RegID = regID;
            TradePwd = pwd;
        }



    }

    [XmlType]
    public class BrokerConfig
    {
        /// <summary>
        /// Broker类型
        /// </summary>
        [XmlAttribute]
        public string BrokerType { get; set; }

        /// <summary>
        /// 资金帐号
        /// </summary>
        [XmlAttribute]
        public string AccountID { get; set; }

        /// <summary>
        /// 资金密码
        /// </summary>
        [XmlAttribute]
        public string AccountPwd { get; set; }

        [XmlArray]
        public List<AccountConfig> Accounts { get; set; }

        public BrokerConfig()
        {
            Accounts = new List<AccountConfig>();
        }
    }



    [XmlType]
    public class RootNetConfig
    {
        /// <summary>
        /// 柜员代码
        /// </summary>
        [XmlAttribute]
        public string OptID { get; set; }

        /// <summary>
        /// 柜员口令
        /// </summary>
        [XmlAttribute]
        public string OptPwd { get; set; }

        /// <summary>
        /// 委托方式
        /// </summary>
        [XmlAttribute]
        public string OptMode { get; set; }


        [XmlArray]
        public List<BrokerConfig> Brokers { get; set; }

        public RootNetConfig()
        {
            Brokers = new List<BrokerConfig>();
        }

    }
}
