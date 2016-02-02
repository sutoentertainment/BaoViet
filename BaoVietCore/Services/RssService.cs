using BaoVietCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaoVietCore.Models;
using BaoVietCore.Factory;

namespace BaoVietCore.Services
{
    public class RssService : ServiceBase, IRssSource
    {
        IXmlParser XmlParser;
        public RssService(Manager man) : base(man)
        {

        }
        public async Task<IEnumerable<IFeedItem>> GetFeed(IXmlParser parser, string source)
        {
            XmlParser = parser;
            var xml = await this.Manager.WebService.GetString(source);
            XmlParser.Load(xml);
            var result = XmlParser.GetFeed();
            return result;
        }
    }
}
