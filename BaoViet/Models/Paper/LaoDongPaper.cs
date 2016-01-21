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
    public class LaoDongPaper : PaperBase
    {
        public LaoDongPaper(PaperType type) : base(type)
        {
            //FrontPagePaper.Add(new VnExpressPaper() { Title = "Lao động", Type = PaperType.LaoDong, HomePage = "http://laodong.com.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-laodong.png" });
            Title = "Lao động";
            HomePage = "http://laodong.com.vn/";
            ImageSource = "ms-appx:///Assets/Logo/logo-laodong.png";


            Categories.Add(new Category("Trang chủ", "http://laodong.com.vn/rss/home.rss"));
            Categories.Add(new Category("Video", "http://laodong.com.vn/rss/video-161.rss"));
            Categories.Add(new Category("Thời sự - Xã hội", "http://laodong.com.vn/rss/thoi-su-xa-hoi-1321.rss"));
            Categories.Add(new Category("Công đoàn", "http://laodong.com.vn/rss/cong-doan-58.rss"));
            Categories.Add(new Category("Thế giới", "http://laodong.com.vn/rss/the-gioi-62.rss"));
            Categories.Add(new Category("Pháp luật", "http://laodong.com.vn/rss/phap-luat-65.rss"));
            Categories.Add(new Category("Văn hóa - Thể thao", "http://laodong.com.vn/rss/van-hoa-the-thao-1322.rss"));
            Categories.Add(new Category("Công nghệ", "http://laodong.com.vn/rss/cong-nghe-66.rss"));
            Categories.Add(new Category("Khám phá", "http://laodong.com.vn/rss/kham-pha-108.rss"));
            Categories.Add(new Category("Vũ khí", "http://laodong.com.vn/rss/vu-khi-107.rss"));
            Categories.Add(new Category("Sức khỏe", "http://laodong.com.vn/rss/suc-khoe-1166.rss"));
            Categories.Add(new Category("Kinh tế", "http://laodong.com.vn/rss/kinh-te-63.rss"));
            Categories.Add(new Category("Bất động sản", "http://laodong.com.vn/rss/bat-dong-san-64.rss"));
            Categories.Add(new Category("Xe+", "http://laodong.com.vn/rss/xe-105.rss"));
            Categories.Add(new Category("Photo", "http://laodong.com.vn/rss/photo-160.rss"));


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
