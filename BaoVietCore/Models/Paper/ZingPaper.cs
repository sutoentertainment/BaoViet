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
    public class ZingPaper : PaperBase
    {
        public ZingPaper(PaperType type) : base(type)
        {
            //FrontPagePaper.Add(new VnExpressPaper() { Title = "Zing news", Type = PaperType.Zing, HomePage = "http://news.zing.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-zing.png" });
            Title = "Zing news";
            HomePage = "http://news.zing.vn/";
            ImageSource = "ms-appx:///Assets/Logo/logo-zing.png";


            Categories.Add(new Category("Tin tức mới nhất", "http://news.zing.vn/rss/tin-moi.rss"));
            Categories.Add(new Category("Trang chủ", "http://news.zing.vn/rss/trang-chu.rss"));
            Categories.Add(new Category("Xã hội", "http://feeds.feedburner.com/zingnews/xa-hoi"));
            Categories.Add(new Category("Thế giới", "http://feeds.feedburner.com/zingnews/the-gioi"));
            Categories.Add(new Category("Thị trường", "http://feeds.feedburner.com/zingnews/thi-truong"));
            Categories.Add(new Category("Pháp luật", "http://feeds.feedburner.com/zingnews/phap-luat"));
            Categories.Add(new Category("Thế giới sạch", "http://feeds.feedburner.com/zingnews/the-gioi-sach"));
            Categories.Add(new Category("Thể thao", "http://feeds.feedburner.com/zingnews/the-thao"));
            Categories.Add(new Category("Công nghệ", "http://feeds.feedburner.com/zingnews/cong-nghe"));
            Categories.Add(new Category("Ô tô - xe máy", "http://feeds.feedburner.com/zingnews/oto-xe-may"));
            Categories.Add(new Category("Giải trí", "http://feeds.feedburner.com/zingnews/giai-tri"));
            Categories.Add(new Category("Âm nhạc", "http://feeds.feedburner.com/zingnews/am-nhac"));
            Categories.Add(new Category("Phim ảnh", "http://feeds.feedburner.com/zingnews/phim-anh"));
            Categories.Add(new Category("Thời trang", "http://feeds.feedburner.com/zingnews/thoi-trang"));
            Categories.Add(new Category("Sống trẻ", "http://feeds.feedburner.com/zingnews/song-tre"));
            Categories.Add(new Category("Giáo dục", "http://feeds.feedburner.com/zingnews/giao-duc"));
            Categories.Add(new Category("Sức khỏe", "http://feeds.feedburner.com/zingnews/suc-khoe"));
            Categories.Add(new Category("Du lịch", "http://feeds.feedburner.com/zingnews/du-lich"));
            Categories.Add(new Category("Ẩm thực", "http://feeds.feedburner.com/zingnews/am-thuc"));


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
                    var sub = item.Descendants().Where(e => e.Name == "enclosure").FirstOrDefault();
                    feed.Thumbnail = sub.Attribute("url").Value;
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
