using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace TangleTrading.Adapter
{
    [MetadataAttribute,AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class BrokerAttribute : ExportAttribute, IBrokerAttribute
    {
        //public Type BrokerType { get; private set; }

        //public string[] Exchanges { get; private set; }

        public string Name { get; set; }

        public Type ConfigType { get; set; }

        public BrokerAttribute(string name, Type cfgType)
        {
            Name = name;
            ConfigType = cfgType;
        }


    }
}
