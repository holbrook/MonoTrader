using System;
namespace TangleTrading.Adapter
{
    public class FeedEventArgs:EventArgs
    {
        public string InstrumentID { get; protected set; }
        public object Event { get; protected set; }

        public FeedEventArgs(string instrumentID, object e)
        {
            InstrumentID = instrumentID;
            Event = e;
        }

    }
}
