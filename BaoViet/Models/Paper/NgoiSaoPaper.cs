using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BaoViet.Models.Paper
{
    public class NgoiSaoPaper : PaperBase
    {
        public NgoiSaoPaper(PaperType type) : base(type)
        {
            //FrontPagePaper.Add(new VnExpressPaper() { Title = "Ngôi sao", Type = PaperType.NgôiSao, HomePage = "http://ngoisao.net", ImageSource = "ms-appx:///Assets/Logo/logo-ngoisao.png" });

            Title = "Ngôi sao";
            HomePage = "http://ngoisao.net";
            ImageSource = "ms-appx:///Assets/Logo/logo-ngoisao.png";


            Categories.Add(new Category("Hậu trường", "http://ngoisao.net/rss/hau-truong.rss"));
            Categories.Add(new Category("Bên lề", "http://ngoisao.net/rss/ben-le.rss"));
            Categories.Add(new Category("Thời cuộc", "http://ngoisao.net/rss/thoi-cuoc.rss"));
            Categories.Add(new Category("Phong cách", "http://ngoisao.net/rss/phong-cach.rss"));
            Categories.Add(new Category("Thư giãn", "http://ngoisao.net/rss/thu-gian.rss"));
            Categories.Add(new Category("Cưới", "http://ngoisao.net/rss/cuoi-hoi.rss"));
            Categories.Add(new Category("Showbiz Việt", "http://ngoisao.net/rss/showbiz-viet.rss"));
            Categories.Add(new Category("Châu Á", "http://ngoisao.net/rss/chau-a.rss"));
            Categories.Add(new Category("Hollywood", "http://ngoisao.net/rss/hollywood.rss"));
            Categories.Add(new Category("Clip", "http://ngoisao.net/rss/clip.rss"));
            Categories.Add(new Category("Khỏe đẹp", "http://ngoisao.net/rss/khoe-dep.rss"));
            Categories.Add(new Category("24h", "http://ngoisao.net/rss/24h.rss"));
            Categories.Add(new Category("Chuyện lạ", "http://ngoisao.net/rss/chuyen-la.rss"));
            Categories.Add(new Category("Hình sự", "http://ngoisao.net/rss/hinh-su.rss"));
            Categories.Add(new Category("Thương trường", "http://ngoisao.net/rss/thuong-truong.rss"));
            Categories.Add(new Category("Thời trang", "http://ngoisao.net/rss/thoi-trang.rss"));
            Categories.Add(new Category("Làm đẹp", "http://ngoisao.net/rss/lam-dep.rss"));
            Categories.Add(new Category("Trắc nghiệm", "http://ngoisao.net/rss/trac-nghiem.rss"));
            Categories.Add(new Category("Ăn chơi", "http://ngoisao.net/rss/an-choi.rss"));
            Categories.Add(new Category("Dân chơi", "http://ngoisao.net/rss/dan-choi.rss"));
            Categories.Add(new Category("Cười", "http://ngoisao.net/rss/cuoi.rss"));
            Categories.Add(new Category("Game", "http://ngoisao.net/rss/game.rss"));
            Categories.Add(new Category("Chơi blog", "http://ngoisao.net/rss/choi-blog.rss"));
            Categories.Add(new Category("Thi ảnh", "http://ngoisao.net/rss/thi-anh.rss"));
            Categories.Add(new Category("Miss", "http://ngoisao.net/rss/miss.rss"));
            Categories.Add(new Category("Cô dâu", "http://ngoisao.net/rss/co-dau.rss"));
            Categories.Add(new Category("Cẩm nang", "http://ngoisao.net/rss/cam-nang.rss"));
            Categories.Add(new Category("Ảnh cưới", "http://ngoisao.net/rss/anh-cuoi.rss"));
            Categories.Add(new Category("Chia sẻ", "http://ngoisao.net/rss/chia-se.rss"));


            foreach (var item in Categories)
            {
                item.Owner = this;
            }
        }

        public override async Task<IEnumerable<FeedItem>> GetFeed(string url)
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

            return feeds;
        }
    }
}
