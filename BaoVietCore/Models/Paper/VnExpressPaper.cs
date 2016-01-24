using BaoVietCore.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Windows.UI.Xaml;
using System.Xml.Linq;
using HtmlAgilityPack;
using System.Net;

namespace BaoVietCore.Models
{
    public class VnExpressPaper : PaperBase
    {
        public VnExpressPaper(PaperType type) : base(type)
        {
            Title = "VnExpress";
            HomePage = "http://vnexpress.net";
            ImageSource = "ms-appx:///Assets/Logo/logo-vne.png";


            Categories.Add(new Category("Trang chủ", "http://vnexpress.net/rss/tin-moi-nhat.rss"));
            Categories.Add(new Category("Thời sự", "http://vnexpress.net/rss/thoi-su.rss"));
            Categories.Add(new Category("Thế giới", "http://vnexpress.net/rss/the-gioi.rss"));
            Categories.Add(new Category("Kinh doanh", "http://vnexpress.net/rss/kinh-doanh.rss"));
            Categories.Add(new Category("Giải trí", "http://vnexpress.net/rss/giai-tri.rss"));
            Categories.Add(new Category("Thể thao", "http://vnexpress.net/rss/the-thao.rss"));
            Categories.Add(new Category("Pháp luật", "http://vnexpress.net/rss/phap-luat.rss"));
            Categories.Add(new Category("Giáo dục", "http://vnexpress.net/rss/giao-duc.rss"));
            Categories.Add(new Category("Sức khỏe", "http://vnexpress.net/rss/suc-khoe.rss"));
            Categories.Add(new Category("Gia đình", "http://vnexpress.net/rss/gia-dinh.rss"));
            Categories.Add(new Category("Du lịch", "http://vnexpress.net/rss/du-lich.rss"));
            Categories.Add(new Category("Khoa học", "http://vnexpress.net/rss/khoa-hoc.rss"));
            Categories.Add(new Category("Số hóa", "http://vnexpress.net/rss/so-hoa.rss"));
            Categories.Add(new Category("Xe", "http://vnexpress.net/rss/oto-xe-may.rss"));
            Categories.Add(new Category("Cộng đồng", "http://vnexpress.net/rss/cong-dong.rss"));
            Categories.Add(new Category("Tâm sự", "http://vnexpress.net/rss/tam-su.rss"));
            Categories.Add(new Category("Cười", "http://vnexpress.net/rss/cuoi.rss"));

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
