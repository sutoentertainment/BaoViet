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
    public class CongAnNhanDanPaper : PaperBase
    {
        public CongAnNhanDanPaper(PaperType type) : base(type)
        {
            //FrontPagePaper.Add(new VnExpressPaper() { Title = "Công An Nhân Dân", Type = PaperType.CongAnNhanDan, HomePage = "http://cand.com.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-cand.png" });
            Title = "Công An Nhân Dân";
            HomePage = "http://cand.com.vn/";
            ImageSource = "ms-appx:///Assets/Logo/logo-cand.png";


            Categories.Add(new Category("Trang chủ", "http://cand.com.vn/rss/trang-chu"));
            Categories.Add(new Category("Thời sự - Chính trị", "http://cand.com.vn/rss/thoi-su"));
            Categories.Add(new Category("Tin tức - Sự kiện", "http://cand.com.vn/rss/Su-kien-Binh-luan-thoi-su"));
            Categories.Add(new Category("Nhịp cầu nhân ái", "http://cand.com.vn/rss/nhip-cau-nhan-ai"));
            Categories.Add(new Category("Kinh tế", "http://cand.com.vn/rss/Kinh-te"));
            Categories.Add(new Category("Địa ốc", "http://cand.com.vn/rss/dia-oc"));
            Categories.Add(new Category("Tài chính - Ngân hàng", "http://cand.com.vn/rss/tai-chinh-ngan-hang"));
            Categories.Add(new Category("Doanh nghiệp", "http://cand.com.vn/rss/doanh-nghiep"));
            Categories.Add(new Category("Khoa học - Công nghệ", "http://cand.com.vn/rss/Khoa-hoc"));
            Categories.Add(new Category("Công nghệ", "http://cand.com.vn/rss/the-gioi-so"));
            Categories.Add(new Category("Thế giới phương tiện", "http://cand.com.vn/rss/the-gioi-phuong-tien"));
            Categories.Add(new Category("Công an trong lòng dân", "http://cand.com.vn/rss/Cong-an"));
            Categories.Add(new Category("Gương sáng", "http://cand.com.vn/rss/Guong-sang"));
            Categories.Add(new Category("Hoạt động LL CAND", "http://cand.com.vn/rss/Hoat-dong-LL-CAND"));
            Categories.Add(new Category("Xã hội", "http://cand.com.vn/rss/Xa-hoi"));
            Categories.Add(new Category("Phóng sự - Tư liệu", "http://cand.com.vn/rss/Phong-su-tu-lieu"));
            Categories.Add(new Category("Y tế", "http://cand.com.vn/rss/y-te"));
            Categories.Add(new Category("Giáo dục", "http://cand.com.vn/rss/giao-duc"));
            Categories.Add(new Category("Giao thông", "http://cand.com.vn/rss/Giao-thong"));
            Categories.Add(new Category("Quốc tế", "http://cand.com.vn/rss/Quoc-te"));
            Categories.Add(new Category("Hồ sơ - Tư liệu", "http://cand.com.vn/rss/tu-lieu-quoc-te"));
            Categories.Add(new Category("Bình luận quốc tế", "http://cand.com.vn/rss/Binh-luan-quoc-te"));
            Categories.Add(new Category("Pháp luật", "http://cand.com.vn/rss/Phap-luat"));
            Categories.Add(new Category("Thông tin pháp luật", "http://cand.com.vn/rss/Thong-tin-phap-luat"));
            Categories.Add(new Category("Bản tin 113", "http://cand.com.vn/rss/ANTT"));
            Categories.Add(new Category("Thể thao", "http://cand.com.vn/rss/The-thao"));
            Categories.Add(new Category("Ống nhòm", "http://cand.com.vn/rss/so-tay-the-thao"));
            Categories.Add(new Category("Thể thao quốc tế", "http://cand.com.vn/rss/The-thao-quoc-te"));
            Categories.Add(new Category("Thư giãn", "http://cand.com.vn/rss/Thu-gian"));
            Categories.Add(new Category("Đời sống", "http://cand.com.vn/rss/doi-song"));
            Categories.Add(new Category("Hôn nhân gia đình", "http://cand.com.vn/rss/hon-nhan-gia-dinh"));
            Categories.Add(new Category("Bạn đọc & CAND", "http://cand.com.vn/rss/Ban-doc-cand"));
            Categories.Add(new Category("Văn hóa - Giải trí", "http://cand.com.vn/rss/van-hoa"));
            Categories.Add(new Category("Video Clip", "http://cand.com.vn/rss/video"));



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
