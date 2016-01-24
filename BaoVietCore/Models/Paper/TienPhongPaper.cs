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
    public class TienPhongPaper : PaperBase
    {
        public TienPhongPaper(PaperType type) : base(type)
        {
            //FrontPagePaper.Add(new VnExpressPaper() { Title = "Tiền phong", Type = PaperType.TienPhong, HomePage = "http://www.tienphong.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-tienphong.png" });
            Title = "Tiền phong";
            HomePage = "http://www.tienphong.vn/";
            ImageSource = "ms-appx:///Assets/Logo/logo-tienphong.png";


            Categories.Add(new Category("Xã hội", "http://www.tienphong.vn/rss/xa-hoi-2.rss"));
            Categories.Add(new Category("Kinh tế", "http://www.tienphong.vn/rss/kinh-te-3.rss"));
            Categories.Add(new Category("Thế giới", "http://www.tienphong.vn/rss/the-gioi-5.rss"));
            Categories.Add(new Category("Giới trẻ", "http://www.tienphong.vn/rss/gioi-tre-4.rss"));
            Categories.Add(new Category("Pháp luật", "http://www.tienphong.vn/rss/phap-luat-12.rss"));
            Categories.Add(new Category("Thể thao", "http://www.tienphong.vn/rss/the-thao-11.rss"));
            Categories.Add(new Category("Văn nghệ", "http://www.tienphong.vn/rss/van-nghe-7.rss"));
            Categories.Add(new Category("Giải trí", "http://www.tienphong.vn/rss/giai-tri-36.rss"));
            Categories.Add(new Category("Giáo dục", "http://www.tienphong.vn/rss/giao-duc-71.rss"));
            Categories.Add(new Category("Khoa học", "http://www.tienphong.vn/rss/cong-nghe-45.rss"));
            Categories.Add(new Category("Người lính", "http://www.tienphong.vn/rss/hanh-trang-nguoi-linh-182.rss"));


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
