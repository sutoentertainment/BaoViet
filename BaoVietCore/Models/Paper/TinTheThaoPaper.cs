using HtmlAgilityPack;
using BaoVietCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BaoVietCore.Models.Paper
{
    public class TinTheThaoPaper : PaperBase
    {
        public TinTheThaoPaper(PaperType type) : base(type)
        {
            //FrontPagePaper.Add(new VnExpressPaper() { Title = "Tin thể thao", Type = PaperType.TinTheThao, HomePage = "http://www.tinthethao.com.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-tinthethao.png" });
            Title = "Tin thể thao";
            HomePage = "http://www.tinthethao.com.vn/";
            ImageSource = "ms-appx:///Assets/Logo/logo-tinthethao.png";


            Categories.Add(new Category("Tin mới nhất", "http://feeds.feedburner.com/com/KYlp"));
            Categories.Add(new Category("Quần Vợt", "http://feeds.feedburner.com/com/dfieq"));
            Categories.Add(new Category("Ôtô-Xe máy", "http://feeds.feedburner.com/com/offiwi"));


            foreach (var item in Categories)
            {
                item.Owner = this;
            }
        }

        public override async Task<RssResult> GetFeed(string url)
        {
            var xml = await Manager.Current.WebService.GetString(url);
            XDocument docs = XDocument.Parse(xml, LoadOptions.None);

            var nodes = docs.Descendants().Where(n => n.Name == "item");

            var feeds = new List<IFeedItem>();
            foreach (var item in nodes)
            {
                var feed = new FeedItem();
                feed.Title = WebUtility.HtmlDecode(item.Descendants().Where(e => e.Name == "title").FirstOrDefault().Value);
                var description = item.Descendants().Where(e => e.Name == "description").FirstOrDefault();

                HtmlDocument htmldocs = new HtmlDocument();
                htmldocs.LoadHtml(description.Value);

                feed.Description = WebUtility.HtmlDecode(htmldocs.DocumentNode.InnerText).Trim();
                try
                {
                    feed.Thumbnail = item.Descendants().Where(e => e.Name == "thumb").FirstOrDefault().Value;
                }
                catch
                {

                }
                feed.Link = item.Descendants().Where(e => e.Name == "link").FirstOrDefault().Value.Trim();

                feeds.Add(feed);
            }

            return new RssResult() { Feeds = feeds, Paper = this.Type };
        }
    }
}
