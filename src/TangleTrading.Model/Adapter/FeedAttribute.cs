using System;
using System.ComponentModel.Composition;

namespace TangleTrading.Adapter
{
    [MetadataAttribute, AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class FeedAttribute : ExportAttribute, IFeedMeta
    {
        public FeedAttribute()
        {
        }
    }
}
