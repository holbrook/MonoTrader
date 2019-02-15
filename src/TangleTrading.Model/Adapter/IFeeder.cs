using System;
namespace TangleTrading.Adapter
{
    public interface IFeeder
    {
        void Subscribe(string code, string type);
        void UnSubscribe(string code, string type);
    }

    public interface IFeed<T>
    {
        event EventHandler<FeedEventArgs<T>> OnEvent;
        
    }
}
