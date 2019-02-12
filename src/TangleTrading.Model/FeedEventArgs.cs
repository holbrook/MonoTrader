using System;
namespace TangleTrading.Model
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
