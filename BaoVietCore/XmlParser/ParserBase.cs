using BaoVietCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoVietCore.XmlParser
{
    public abstract class ParserBase : IXmlParser
    {
        protected string xml;

        public ParserBase(string xml)
        {
            this.xml = xml;
        }

        public IEnumerable<IFeedItem> GetFeed()
        {
            throw new NotImplementedException();
        }

        public void Load(string xml)
        {
            throw new NotImplementedException();
        }
    }
}
