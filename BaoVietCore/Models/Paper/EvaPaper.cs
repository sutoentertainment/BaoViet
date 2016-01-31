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
    public class EvaPaper : PaperBase
    {
        public EvaPaper(PaperType type) : base(type)
        {
            //FrontPagePaper.Add(new VnExpressPaper() { Title = "Eva", Type = PaperType.Eva, HomePage = "http://eva.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-eva.png" });
            Title = "Eva";
            HomePage = "http://eva.vn/";
            ImageSource = "ms-appx:///Assets/Logo/logo-eva.png";


            Categories.Add(new Category("Trang chủ Eva", "http://eva.vn/rss/rss.php"));
            Categories.Add(new Category("Tin tức", "http://eva.vn/rss/rss.php/73"));
            Categories.Add(new Category("Eva tám", "http://eva.vn/rss/rss.php/66"));
            Categories.Add(new Category("Làng sao", "http://eva.vn/rss/rss.php/20"));
            Categories.Add(new Category("Thời trang", "http://eva.vn/rss/rss.php/36"));
            Categories.Add(new Category("Làm đẹp", "http://eva.vn/rss/rss.php/58"));
            Categories.Add(new Category("Bà bầu", "http://eva.vn/rss/rss.php/85"));
            Categories.Add(new Category("Làm mẹ", "http://eva.vn/rss/rss.php/10"));
            Categories.Add(new Category("Tình yêu - Giới tính", "http://eva.vn/rss/rss.php/3"));
            Categories.Add(new Category("Bếp Eva", "http://eva.vn/rss/rss.php/162"));
            Categories.Add(new Category("Nhà đẹp", "http://eva.vn/rss/rss.php/169"));
            Categories.Add(new Category("Clip Eva", "http://eva.vn/rss/rss.php/157"));
            Categories.Add(new Category("Sức khỏe", "http://eva.vn/rss/rss.php/131"));
            Categories.Add(new Category("Đi đâu - Xem gì", "http://eva.vn/rss/rss.php/40"));
            Categories.Add(new Category("Mua sắm - Giá cả", "http://eva.vn/rss/rss.php/2"));
            Categories.Add(new Category("Eva Sành điệu", "http://eva.vn/rss/rss.php/173"));
            Categories.Add(new Category("Lịch Vạn Niên", "http://eva.vn/rss/rss.php/192"));
            Categories.Add(new Category("Ảnh đẹp Eva", "http://eva.vn/rss/rss.php/186"));


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

                feed.Description = WebUtility.HtmlDecode(htmldocs.DocumentNode.InnerText);
                try
                {
                    feed.Thumbnail = htmldocs.DocumentNode.Descendants("img").FirstOrDefault().Attributes["src"].Value;
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
