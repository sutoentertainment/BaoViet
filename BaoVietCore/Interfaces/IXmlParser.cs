using BaoVietCore.Interfaces;
using System.Collections.Generic;

namespace BaoVietCore.Interfaces
{
    public interface IXmlParser
    {
        IEnumerable<IFeedItem> GetFeed();
        void Load(string xml);
    }
}