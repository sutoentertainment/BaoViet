using System;
using BaoVietCore.Interfaces;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using System.Net;
using BaoVietCore.XmlParser;
using BaoVietCore.Models;

namespace BaoVietCore.XmlParser
{
    /// <summary>
    /// Class for xml parsing based on DanTri feed format, widely used by other papers.
    /// </summary>
    internal class BBCParser : IXmlParser
    {
        private string xml;

        public BBCParser()
        {

        }

        public BBCParser(string xml)
        {
            this.xml = xml;
        }

        public IEnumerable<IFeedItem> GetFeed()
        {
            XDocument docs = XDocument.Parse(xml, LoadOptions.None);

            var nodes = docs.Descendants().Where(n => n.Name.LocalName == "entry");

            var feeds = new List<IFeedItem>();
            foreach (var item in nodes)
            {
                var feed = new FeedItem();
                feed.Title = WebUtility.HtmlDecode(item.Descendants().FirstOrDefault(e => e.Name.LocalName == "title").Value);
                var description = item.Descendants().FirstOrDefault(e => e.Name.LocalName == "summary");

                HtmlDocument htmldocs = new HtmlDocument();
                htmldocs.LoadHtml(description.Value);

                feed.Description = WebUtility.HtmlDecode(htmldocs.DocumentNode.InnerText);
                try
                {
                    feed.Thumbnail = htmldocs.DocumentNode.Descendants("img").FirstOrDefault().Attributes["src"].Value;
                }
                catch
                {

                }
                feed.Link = item.Descendants().FirstOrDefault(e => e.Name.LocalName == "link").Attribute("href").Value.Trim();

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