using System;
namespace Tangle.Trading
{
    public interface IFeed
    {
        void Subscribe(string code, string type);
        void UnSubscribe(string code, string type);
    }
}
