using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace TangleTrading.Adapter
{
    [MetadataAttribute,AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class BrokerAttribute : ExportAttribute, IBrokerMeta
    {
        //public Type BrokerType { get; private set; }

        //public string[] Exchanges { get; private set; }

        public Dictionary<Type, string[]> Exchanges { get; private set; }

        public BrokerAttribute(Dictionary<Type, string[]> exchanges)
        {
            Exchanges = exchanges;
        }


    }
}
