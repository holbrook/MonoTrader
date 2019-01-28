using System;
using System.ComponentModel.Composition;

namespace Tangle.Trading
{
    [MetadataAttribute,AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class BrokerAttribute : ExportAttribute, IBrokerMeta
    {
        public Type BrokerType { get; private set; }

        public string[] Markets { get; private set; }

        public BrokerAttribute(Type type, string[] markets)
        {
            BrokerType = type;
            Markets = markets;
        }


    }
}
