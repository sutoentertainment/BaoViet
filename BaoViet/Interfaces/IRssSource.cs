using BaoViet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoViet.Interfaces
{
    public interface IRssSource
    {
        Task<RssResult> GetFeed(string url);
    }

    public class RssResult
    {
        public IEnumerable<FeedItem> Feeds { get; set; }
        public PaperType Paper { get; set; }
    }
}
