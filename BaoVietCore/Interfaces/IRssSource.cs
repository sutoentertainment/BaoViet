using BaoVietCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoVietCore.Interfaces
{
    public interface IRssSource
    {
        Task<IEnumerable<IFeedItem>> GetFeed(IXmlParser parser, string source);
    }

    public class RssResult
    {
        public IEnumerable<IFeedItem> Feeds { get; set; }
    }
}
