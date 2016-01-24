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
    public class ICTNewsPaper : PaperBase
    {
        public ICTNewsPaper(PaperType type) : base(type)
        {
            //FrontPagePaper.Add(new VnExpressPaper() { Title = "ICT news", Type = PaperType.ICTNews, HomePage = "http://www.ictnews.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-ictnews.png" });
            Title = "ICT news";
            HomePage = "http://www.ictnews.vn/";
            ImageSource = "ms-appx:///Assets/Logo/logo-ictnews.png";


            Categories.Add(new Category("Viễn thông", "http://ictnews.vn/rss/vien-thong"));
            Categories.Add(new Category("Cải cách hành chính", "http://ictnews.vn/rss/vien-thong/cai-cach-hanh-chinh"));
            Categories.Add(new Category("Số hóa truyền hình", "http://ictnews.vn/rss/vien-thong/so-hoa-truyen-hinh"));
            Categories.Add(new Category("VNPT - Cuộc sống đích thực", "http://ictnews.vn/rss/vien-thong/vnpt-cuoc-song-dich-thuc"));
            Categories.Add(new Category("Diễn đàn MobiFone", "http://ictnews.vn/rss/vien-thong/dien-dan-mobifone"));
            Categories.Add(new Category("Tiến lên 4G", "http://ictnews.vn/rss/vien-thong/tien-len-4g"));
            Categories.Add(new Category("Internet", "http://ictnews.vn/rss/internet"));
            Categories.Add(new Category("Xã hội @", "http://ictnews.vn/rss/internet/xa-hoi"));
            Categories.Add(new Category("Cải cách hành chính", "http://ictnews.vn/rss/internet/cai-cach-hanh-chinh"));
            Categories.Add(new Category("CNTT", "http://ictnews.vn/rss/cntt"));
            Categories.Add(new Category("Phần mềm", "http://ictnews.vn/rss/cntt/phan-mem"));
            Categories.Add(new Category("Phần cứng", "http://ictnews.vn/rss/cntt/phan-cung"));
            Categories.Add(new Category("Bảo mật", "http://ictnews.vn/rss/cntt/bao-mat"));
            Categories.Add(new Category("Hội nhập", "http://ictnews.vn/rss/cntt/hoi-nhap"));
            Categories.Add(new Category("Nước mạnh CNTT", "http://ictnews.vn/rss/cntt/nuoc-manh-cntt"));
            Categories.Add(new Category("Công nghiệp CNTT", "http://ictnews.vn/rss/cntt/cong-nghiep-cntt"));
            Categories.Add(new Category("Nghị quyết 36-NQ/TW", "http://ictnews.vn/rss/cntt/nghi-quyet-36nqtw"));
            Categories.Add(new Category("Kinh doanh", "http://ictnews.vn/rss/kinh-doanh"));
            Categories.Add(new Category("Hồ sơ", "http://ictnews.vn/rss/kinh-doanh/ho-so"));
            Categories.Add(new Category("Thị trường", "http://ictnews.vn/rss/kinh-doanh/thi-truong"));
            Categories.Add(new Category("Doanh nghiệp", "http://ictnews.vn/rss/kinh-doanh/doanh-nghiep"));
            Categories.Add(new Category("Bạn đọc viết", "http://ictnews.vn/rss/kinh-doanh/ban-doc-viet"));
            Categories.Add(new Category("Thế giới số", "http://ictnews.vn/rss/the-gioi-so"));
            Categories.Add(new Category("Máy ảnh số", "http://ictnews.vn/rss/the-gioi-so/may-anh-so"));
            Categories.Add(new Category("Di động", "http://ictnews.vn/rss/the-gioi-so/di-dong"));
            Categories.Add(new Category("Máy tính", "http://ictnews.vn/rss/the-gioi-so/may-tinh"));
            Categories.Add(new Category("Hình ảnh âm thanh", "http://ictnews.vn/rss/the-gioi-so/hinh-anh-am-thanh"));
            Categories.Add(new Category("Phụ kiện", "http://ictnews.vn/rss/the-gioi-so/phu-kien"));
            Categories.Add(new Category("Thủ thuật", "http://ictnews.vn/rss/the-gioi-so/thu-thuat"));
            Categories.Add(new Category("Game", "http://ictnews.vn/rss/game"));
            Categories.Add(new Category("Khởi nghiệp", "http://ictnews.vn/rss/khoi-nghiep"));
            Categories.Add(new Category("Góc doanh nghiệp", "http://ictnews.vn/rss/khoi-nghiep/goc-doanh-nghiep"));
            Categories.Add(new Category("Câu chuyện khởi nghiệp", "http://ictnews.vn/rss/khoi-nghiep/cau-chuyen-khoi-nghiep"));
            Categories.Add(new Category("Báo cáo, thống kê, các con số", "http://ictnews.vn/rss/khoi-nghiep/bao-cao-thong-ke-cac-con-so"));
            Categories.Add(new Category("Công nghệ 360", "http://ictnews.vn/rss/cong-nghe-360"));
            Categories.Add(new Category("Ô tô – Xe máy", "http://ictnews.vn/rss/cong-nghe-360/o-to-xe-may"));
            Categories.Add(new Category("Điện máy", "http://ictnews.vn/rss/cong-nghe-360/dien-may"));
            Categories.Add(new Category("Khoa học", "http://ictnews.vn/rss/cong-nghe-360/khoa-hoc"));
            Categories.Add(new Category("Hỏi đáp", "http://ictnews.vn/rss/cong-nghe-360/hoi-dap"));



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
