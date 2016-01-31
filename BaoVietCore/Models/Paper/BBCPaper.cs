using BaoVietCore.Interfaces;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BaoVietCore.Models.Paper
{
    public class BBCPaper : PaperBase
    {
        public BBCPaper(PaperType type) : base(type)
        {
            //FrontPagePaper.Add(new VnExpressPaper() { Title = "BBC tiếng Việt", Type = PaperType.BBC, HomePage = "http://www.bbc.com/vietnamese", ImageSource = "ms-appx:///Assets/Logo/logo-bbc.png" });
            Title = "BBC tiếng Việt";
            HomePage = "http://www.bbc.com/vietnamese";
            ImageSource = "ms-appx:///Assets/Logo/logo-bbc.png";


            Categories.Add(new Category("Trang chủ", "http://www.bbc.com/vietnamese/index.xml"));
            Categories.Add(new Category("Việt Nam", "http://www.bbc.com/vietnamese/vietnam/index.xml"));
            Categories.Add(new Category("Thế giới", "http://www.bbc.com/vietnamese/world/index.xml"));
            Categories.Add(new Category("Diễn đàn", "http://www.bbc.com/vietnamese/topics/forum/index.xml"));
            Categories.Add(new Category("Kinh tế", "http://www.bbc.com/vietnamese/business/index.xml"));
            Categories.Add(new Category("Nhịp sống mới", "http://www.bbc.com/vietnamese/topics/magazine/index.xml"));
            Categories.Add(new Category("Thể thao", "http://www.bbc.com/vietnamese/sport/index.xml"));
            Categories.Add(new Category("Học tiếng Anh", "http://www.bbc.com/vietnamese/english/index.xml"));
            Categories.Add(new Category("Hình ảnh", "http://www.bbc.com/vietnamese/topics/photo/index.xml"));
            Categories.Add(new Category("Audio", "http://www.bbc.com/vietnamese/topics/audio/index.xml"));
            Categories.Add(new Category("Video", "http://www.bbc.com/vietnamese/topics/video/index.xml"));


            foreach (var item in Categories)
            {
                item.Owner = this;
            }
        }

        public override async Task<RssResult> GetFeed(string url)
        {
            var xml = await Manager.Current.WebService.GetString(url);
            XDocument docs = XDocument.Parse(xml, LoadOptions.None);

            var nodes = docs.Descendants().Where(n => n.Name.LocalName == "entry");

            var feeds = new List<IFeedItem>();
            foreach (var item in nodes)
            {
                var feed = new FeedItem();
                feed.Title = WebUtility.HtmlDecode(item.Descendants().Where(e => e.Name.LocalName == "title").FirstOrDefault().Value);
                var description = item.Descendants().Where(e => e.Name.LocalName == "summary").FirstOrDefault();

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
                feed.Link = item.Descendants().Where(e => e.Name.LocalName == "link").FirstOrDefault().Attribute("href").Value.Trim();

                feeds.Add(feed);
            }

            return new RssResult() { Feeds = feeds, Paper = this.Type };
        }
    }
}
