using System;
namespace TangleTrading.Adapter
{
    public class FeedEventArgs:EventArgs
    {

    }

    public class FeedEventArgs<T>: FeedEventArgs
    {
        public string InstrumentID { get; private set; }
        public T Event { get; private set; }
        public FeedEventArgs(string instrumentID, T e)
        {
            InstrumentID = instrumentID;
            Event = e;
        }
    }
}
