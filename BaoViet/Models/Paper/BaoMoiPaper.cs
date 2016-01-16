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
    public class BaoMoiPaper : PaperBase
    {
        public BaoMoiPaper(PaperType type) : base(type)
        {
            //FrontPagePaper.Add(new VnExpressPaper() { Title = "Báo mới", Type = PaperType.BaoMoi, HomePage = "http://www.baomoi.com/", ImageSource = "ms-appx:///Assets/Logo/logo-baomoi.png" });
            Title = "Báo mới";
            HomePage = "http://www.baomoi.com/";
            ImageSource = "ms-appx:///Assets/Logo/logo-baomoi.png";

            Categories.Add(new Category("Xã hội", "http://www.baomoi.com/xahoi.rss"));
            Categories.Add(new Category("Thế giới", "http://www.baomoi.com/thegioi.rss"));
            Categories.Add(new Category("Văn hóa", "http://www.baomoi.com/vanhoa.rss"));
            Categories.Add(new Category("Kinh tế", "http://www.baomoi.com/kinhte.rss"));
            Categories.Add(new Category("Giáo dục", "http://www.baomoi.com/giaoduc.rss"));
            Categories.Add(new Category("Thể thao", "http://www.baomoi.com/thethao.rss"));
            Categories.Add(new Category("Giải trí", "http://www.baomoi.com/giaitri.rss"));
            Categories.Add(new Category("Pháp luật", "http://www.baomoi.com/phapluat.rss"));
            Categories.Add(new Category("Khoa học - công nghệ", "http://www.baomoi.com/khcn.rss"));
            Categories.Add(new Category("Sức khỏe", "http://www.baomoi.com/suckhoe.rss"));
            Categories.Add(new Category("Ô tô - Xe máy", "http://www.baomoi.com/otoxemay.rss"));
            Categories.Add(new Category("Nhà đất", "http://www.baomoi.com/nhadat.rss"));


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
