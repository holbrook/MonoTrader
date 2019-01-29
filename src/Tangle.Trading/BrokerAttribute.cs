using System;
using System.ComponentModel.Composition;

namespace Tangle.Trading
{
    [MetadataAttribute,AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class BrokerAttribute : ExportAttribute, IBrokerMeta
    {
        public Type BrokerType { get; private set; }

        public string[] Exchanges { get; private set; }

        public BrokerAttribute(Type type, string[] exchanges)
        {
            BrokerType = type;
            Exchanges = exchanges;
        }


    }
}
