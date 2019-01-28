using System;
namespace Tangle.Trading
{
    public interface IBrokerMeta
    {
        Type BrokerType { get;}
        string[] Markets { get; }
    }
}
