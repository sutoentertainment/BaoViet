using HtmlAgilityPack;
using BaoViet.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BaoViet.Models.Paper
{
    public class VietnamPlusPaper : PaperBase
    {
        public VietnamPlusPaper(PaperType type) : base(type)
        {
            //FrontPagePaper.Add(new VnExpressPaper() { Title = "VietnamPlus", Type = PaperType.VietnamPlus, HomePage = "http://www.vietnamplus.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-vnp.png" });
            Title = "VietnamPlus";
            HomePage = "http://www.vietnamplus.vn/";
            ImageSource = "ms-appx:///Assets/Logo/logo-vnp.png";


            Categories.Add(new Category("Trang chủ", "http://www.vietnamplus.vn/rss/news.rss"));
            Categories.Add(new Category("Kinh tế", "http://www.vietnamplus.vn/rss/kinhte.rss"));
            Categories.Add(new Category("Chính trị", "http://www.vietnamplus.vn/rss/chinhtri.rss"));
            Categories.Add(new Category("Xã hội", "http://www.vietnamplus.vn/rss/xahoi.rss"));
            Categories.Add(new Category("Thế giới", "http://www.vietnamplus.vn/rss/thegioi.rss"));
            Categories.Add(new Category("Đời sống", "http://www.vietnamplus.vn/rss/doisong.rss"));
            Categories.Add(new Category("Văn hóa", "http://www.vietnamplus.vn/rss/vanhoa.rss"));
            Categories.Add(new Category("Thể thao", "http://www.vietnamplus.vn/rss/thethao.rss"));
            Categories.Add(new Category("Khoa học", "http://www.vietnamplus.vn/rss/khoahoc.rss"));
            Categories.Add(new Category("Công nghệ", "http://www.vietnamplus.vn/rss/congnghe.rss"));
            Categories.Add(new Category("Chuyện lạ", "http://www.vietnamplus.vn/rss/chuyenla.rss"));
            Categories.Add(new Category("Rao vặt", "http://www.vietnamplus.vn/rss/raovat.rss"));
            


            foreach (var item in Categories)
            {
                item.Owner = this;
            }
        }

        public override async Task<RssResult> GetFeed(string url)
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

            return new RssResult() { Feeds = feeds, Paper = this.Type };
        }
    }
}
