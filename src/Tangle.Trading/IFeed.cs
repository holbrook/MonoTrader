using System;
namespace Tangle.Trading
{
    public interface IFeed
    { }

    public interface IFeed<T> : IFeed
    {
        event EventHandler<FeedEventArgs<T>> OnEvent;
        void Subscribe(string code, string type);
        void UnSubscribe(string code, string type);
    }
}
