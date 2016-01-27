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
    public class GiaDinhPaper : PaperBase
    {
        public GiaDinhPaper(PaperType type) : base(type)
        {
            //FrontPagePaper.Add(new VnExpressPaper() { Title = "Gia đình", Type = PaperType.GiaDinh, HomePage = "http://giadinh.net.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-giadinh.png" });
            Title = "Gia đình";
            HomePage = "http://giadinh.net.vn/";
            ImageSource = "ms-appx:///Assets/Logo/logo-giadinh.png";


            Categories.Add(new Category("Trang chủ", "http://giadinh.net.vn/home.rss"));
            Categories.Add(new Category("Tranh cãi", "http://giadinh.net.vn/tranh-cai.rss"));
            Categories.Add(new Category("Ảnh của bạn", "http://giadinh.net.vn/anh-cua-ban.rss"));
            Categories.Add(new Category("Ăn", "http://giadinh.net.vn/an.rss"));
            Categories.Add(new Category("Ở", "http://giadinh.net.vn/o.rss"));
            Categories.Add(new Category("Đẹp", "http://giadinh.net.vn/dep.rss"));
            Categories.Add(new Category("Kỹ năng sống", "http://giadinh.net.vn/ky-nang-song.rss"));
            Categories.Add(new Category("Bốn phương", "http://giadinh.net.vn/bon-phuong.rss"));
            Categories.Add(new Category("Gia đình hi-tech", "http://giadinh.net.vn/gia-dinh-hi-tech.rss"));
            Categories.Add(new Category("Xã hội", "http://giadinh.net.vn/xa-hoi.rss"));
            Categories.Add(new Category("Thị trường", "http://giadinh.net.vn/thi-truong.rss"));
            Categories.Add(new Category("Gia đình", "http://giadinh.net.vn/gia-dinh.rss"));
            Categories.Add(new Category("Giáo dục", "http://giadinh.net.vn/giao-duc.rss"));
            Categories.Add(new Category("Phòng the", "http://giadinh.net.vn/phong-the.rss"));
            Categories.Add(new Category("Sống khỏe", "http://giadinh.net.vn/song-khoe.rss"));
            Categories.Add(new Category("Xem Nghe Đọc", "http://giadinh.net.vn/xem-nghe-doc.rss"));
            Categories.Add(new Category("Tâm sự", "http://giadinh.net.vn/tam-su.rss"));
            Categories.Add(new Category("Pháp luật", "http://giadinh.net.vn/phap-luat.rss"));
            Categories.Add(new Category("Vòng tay nhân ái", "http://giadinh.net.vn/vong-tay-nhan-ai.rss"));
            Categories.Add(new Category("Dân số", "http://giadinh.net.vn/dan-so.rss"));
            Categories.Add(new Category("Y tế", "http://giadinh.net.vn/y-te.rss"));


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
