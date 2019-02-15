using System;
namespace TangleTrading.Adapter
{
    public interface IFeeder
    {
        ExecuteStatus Subscribe(string code, string type);
        ExecuteStatus UnSubscribe(string code, string type);

        event EventHandler<FeedEventArgs> FeedEventHandler;
    }

    public interface IFeed<T>
    {
        
        
    }
}
