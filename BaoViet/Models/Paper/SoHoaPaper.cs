using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BaoViet.Models.Paper
{
    public class SoHoaPaper : PaperBase
    {
        public SoHoaPaper(PaperType type) : base(type)
        {
            //FrontPagePaper.Add(new VnExpressPaper() { Title = "Số hóa", Type = PaperType.SốHóa, HomePage = "http://sohoa.vnexpress.net", ImageSource = "ms-appx:///Assets/Logo/logo-sohoa.png" });
            Title = "Số hóa";
            HomePage = "http://sohoa.vnexpress.net";
            ImageSource = "ms-appx:///Assets/Logo/logo-sohoa.png";


            Categories.Add(new Category("Trang chủ", "http://vnexpress.net/rss/so-hoa.rss"));
            


            foreach (var item in Categories)
            {
                item.Owner = this;
            }
        }

        public override async Task<IEnumerable<FeedItem>> GetFeed(string url)
        {
            var xml = await App.WebService.GetString(url);
            XDocument docs = XDocument.Parse(xml, LoadOptions.None);

            var nodes = docs.Descendants().Where(n => n.Name == "item");

            var feeds = new List<FeedItem>();
            foreach (var item in nodes)
            {
                var feed = new FeedItem();
                feed.Title = WebUtility.HtmlDecode(item.Descendants().Where(e => e.Name == "title").FirstOrDefault().Value);
                var description = item.Descendants().Where(e => e.Name == "description").FirstOrDefault();

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
                feed.Link = item.Descendants().Where(e => e.Name == "link").FirstOrDefault().Value;

                feeds.Add(feed);
            }

            return feeds;
        }
    }
}
