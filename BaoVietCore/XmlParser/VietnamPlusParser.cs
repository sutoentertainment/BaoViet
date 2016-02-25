using System;
using BaoVietCore.Interfaces;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using System.Net;

namespace BaoVietCore.Models.XmlParser
{
    /// <summary>
    /// Class for xml parsing based on DanTri feed format, widely used by other papers.
    /// </summary>
    internal class VietnamPlusParser : IXmlParser
    {
        private string xml;

        public VietnamPlusParser()
        {

        }

        public VietnamPlusParser(string xml)
        {
            this.xml = xml;
        }

        public IEnumerable<IFeedItem> GetFeed()
        {
            XDocument docs = XDocument.Parse(xml, LoadOptions.None);

            var nodes = docs.Descendants().Where(n => n.Name == "item");

            var feeds = new List<IFeedItem>();
            foreach (var item in nodes)
            {
                var feed = new FeedItem();
                feed.Title = WebUtility.HtmlDecode(item.Descendants().FirstOrDefault(e => e.Name == "title").Value);
                var description = item.Descendants().FirstOrDefault(e => e.Name == "description");

                HtmlDocument htmldocs = new HtmlDocument();
                htmldocs.LoadHtml(description.Value);

                feed.Description = WebUtility.HtmlDecode(htmldocs.DocumentNode.InnerText);
                try
                {
                    var sub = item.Descendants().Where(e => e.Name.LocalName == "content").FirstOrDefault();
                    feed.Thumbnail = sub.Attribute("url").Value;
                }
                catch
                {

                }
                feed.Link = item.Descendants().FirstOrDefault(e => e.Name == "link").Value.Trim();

                feeds.Add(feed);
            }

            return feeds;
        }

        public void Load(string xml)
        {
            this.xml = xml;
        }
    }
}