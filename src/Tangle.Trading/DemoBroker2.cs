using System;
using System.ComponentModel.Composition;
using Tangle.Trading.NEEQMM;

namespace Tangle.Trading
{
    [Export(typeof(IBroker))]
    [Broker(typeof(INEEQMMBroker), new string[] { "NEEQ" })]

    public class DemoBroker2
    {
        public DemoBroker2()
        {
        }
    }
}
