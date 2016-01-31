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
    public class VietBaoPaper : PaperBase
    {
        public VietBaoPaper(PaperType type) : base(type)
        {

            //FrontPagePaper.Add(new VnExpressPaper() { Title = "Việt báo", Type = PaperType.VietBao, HomePage = "http://vietbao.vn", ImageSource = "ms-appx:///Assets/Logo/logo-vietbao.png" });
            Title = "Việt báo";
            HomePage = "http://dantri.com.vn";
            ImageSource = "ms-appx:///Assets/Logo/logo-vietbao.png";


            Categories.Add(new Category("Trang Nhất", "http://vietbao.vn/rss2/trang-nhat.rss"));
            Categories.Add(new Category("Tin Mới Nhất trên Việt Báo", "http://vietbao.vn/rss2/tinmoi.rss"));
            Categories.Add(new Category("An Ninh - Pháp Luật", "http://vietbao.vn/live/An-ninh-Phap-luat/rss.xml"));
            Categories.Add(new Category("Blog Hay", "http://vietbao.vn/live/Blog/rss.xml"));
            Categories.Add(new Category("Bóng Đá", "http://vietbao.vn/live/Bong-da/rss.xml"));
            Categories.Add(new Category("Chiêm Tinh", "http://vietbao.vn/live/Chiem-tinh/rss.xml"));
            Categories.Add(new Category("Công Nghệ", "http://vietbao.vn/live/Cong-nghe/rss.xml"));
            Categories.Add(new Category("Du Lịch", "http://vietbao.vn/live/Du-lich/rss.xml"));
            Categories.Add(new Category("Đời Sống-Gia Đình", "http://vietbao.vn/live/Doi-song-Gia-dinh/rss.xml"));
            Categories.Add(new Category("Game", "http://vietbao.vn/live/Game/rss.xml"));
            Categories.Add(new Category("Giáo Dục", "http://vietbao.vn/live/Giao-duc/rss.xml"));
            Categories.Add(new Category("Khám Phá Việt Nam", "http://vietbao.vn/live/Kham-pha-Viet-Nam/rss.xml"));
            Categories.Add(new Category("Khoa Học", "http://vietbao.vn/live/Khoa-hoc/rss.xml"));
            Categories.Add(new Category("Kinh Tế", "http://vietbao.vn/live/Kinh-te/rss.xml"));
            Categories.Add(new Category("Người Việt Bốn Phương", "http://vietbao.vn/live/Nguoi-Viet-bon-phuong/rss.xml"));
            Categories.Add(new Category("Nhà Đất", "http://vietbao.vn/live/Nha-dat/rss.xml"));
            Categories.Add(new Category("Ô tô - Xe Máy", "http://vietbao.vn/live/O-to-xe-may/rss.xml"));
            Categories.Add(new Category("Phóng Sự", "http://vietbao.vn/live/Phong-su/rss.xml"));
            Categories.Add(new Category("Sống Đẹp", "http://vietbao.vn/live/Dep/rss.xml"));
            Categories.Add(new Category("Sức Khỏe", "http://vietbao.vn/live/Suc-khoe/rss.xml"));
            Categories.Add(new Category("Tết", "http://vietbao.vn/live/Tet/rss.xml"));
            Categories.Add(new Category("Thế Giới", "http://vietbao.vn/live/The-gioi/rss.xml"));
            Categories.Add(new Category("Thế Giới Giải Trí", "http://vietbao.vn/live/The-gioi-giai-tri/rss.xml"));
            Categories.Add(new Category("Thế Giới Trẻ", "http://vietbao.vn/live/The-gioi-tre/rss.xml"));
            Categories.Add(new Category("Thể Thao", "http://vietbao.vn/live/The-thao/rss.xml"));
            Categories.Add(new Category("Trang Ban Đọc", "http://vietbao.vn/live/Trang-ban-doc/rss.xml"));
            Categories.Add(new Category("Tuyển Sinh", "http://vietbao.vn/live/Tuyen-sinh/rss.xml"));
            Categories.Add(new Category("Văn Hóa", "http://vietbao.vn/live/Van-hoa/rss.xml"));
            Categories.Add(new Category("Việc Làm", "http://vietbao.vn/live/Viec-lam/rss.xml"));
            Categories.Add(new Category("Vui Cười", "http://vietbao.vn/live/Cuoi/rss.xml"));
            Categories.Add(new Category("Xã Hội", "http://vietbao.vn/live/Xa-hoi/rss.xml"));


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
