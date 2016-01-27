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
    public class Hai4hPaper : PaperBase
    {
        public Hai4hPaper(PaperType type) : base(type)
        {

            //FrontPagePaper.Add(new VnExpressPaper() { Title = "24h", Type = PaperType.Báo24H, HomePage = "http://24h.com.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-24h.png"});
            Title = "24h";
            HomePage = "http://24h.com.vn";
            ImageSource = "ms-appx:///Assets/Logo/logo-24h.png";


            Categories.Add(new Category("Tin tức trong ngày", "http://www.24h.com.vn/upload/rss/tintuctrongngay.rss"));
            Categories.Add(new Category("Bóng đá", "http://www.24h.com.vn/upload/rss/bongda.rss"));
            Categories.Add(new Category("An ninh - Hình sự", "http://www.24h.com.vn/upload/rss/anninhhinhsu.rss"));
            Categories.Add(new Category("Thời trang", "http://www.24h.com.vn/upload/rss/thoitrang.rss"));
            Categories.Add(new Category("Thời trang Hi-tech", "http://www.24h.com.vn/upload/rss/thoitranghitech.rss"));
            Categories.Add(new Category("Tài chính – Bất động sản", "http://www.24h.com.vn/upload/rss/taichinhbatdongsan.rss"));
            Categories.Add(new Category("Ẩm thực", "http://www.24h.com.vn/upload/rss/amthuc.rss"));
            Categories.Add(new Category("Làm đẹp", "http://www.24h.com.vn/upload/rss/lamdep.rss"));
            Categories.Add(new Category("Phim", "http://www.24h.com.vn/upload/rss/phim.rss"));
            Categories.Add(new Category("Giáo dục - du học", "http://www.24h.com.vn/upload/rss/giaoducduhoc.rss"));
            Categories.Add(new Category("Bạn trẻ - Cuộc sống", "http://www.24h.com.vn/upload/rss/bantrecuocsong.rss"));
            Categories.Add(new Category("Ca nhạc - MTV", "http://www.24h.com.vn/upload/rss/canhacmtv.rss"));
            Categories.Add(new Category("Thể thao", "http://www.24h.com.vn/upload/rss/thethao.rss"));
            Categories.Add(new Category("Phi thường - kỳ quặc", "http://www.24h.com.vn/upload/rss/phithuongkyquac.rss"));
            Categories.Add(new Category("Công nghệ thông tin", "http://www.24h.com.vn/upload/rss/congnghethongtin.rss"));
            Categories.Add(new Category("Ô tô - Xe máy", "http://www.24h.com.vn/upload/rss/otoxemay.rss"));
            Categories.Add(new Category("Thị trường - Tiêu dùng", "http://www.24h.com.vn/upload/rss/thitruongtieudung.rss"));
            Categories.Add(new Category("Du lịch", "http://www.24h.com.vn/upload/rss/dulich.rss"));
            Categories.Add(new Category("Sức khỏe đời sống", "http://www.24h.com.vn/upload/rss/suckhoedoisong.rss"));
            Categories.Add(new Category("Cười 24h", "http://www.24h.com.vn/upload/rss/cuoi24h.rss"));
            Categories.Add(new Category("Thế giới", "http://www.24h.com.vn/upload/rss/tintucquocte.rss"));
            Categories.Add(new Category("Đời sống Showbiz", "http://www.24h.com.vn/upload/rss/doisongshowbiz.rss"));
            Categories.Add(new Category("Giải trí", "http://www.24h.com.vn/upload/rss/giaitri.rss"));


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
