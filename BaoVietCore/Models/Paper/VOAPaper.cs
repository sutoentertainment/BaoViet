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
    public class VOAPaper : PaperBase
    {
        public VOAPaper(PaperType type) : base(type)
        {
            //FrontPagePaper.Add(new VnExpressPaper() { Title = "VOA tiếng Việt", Type = PaperType.VOA, HomePage = "http://www.voatiengviet.com/", ImageSource = "ms-appx:///Assets/Logo/logo-voa.png" });
            Title = "VOA tiếng Việt";
            HomePage = "http://www.voatiengviet.com/";
            ImageSource = "ms-appx:///Assets/Logo/logo-voa.png";


            Categories.Add(new Category("Trang chủ", "http://www.voatiengviet.com/api/epiqq"));
            Categories.Add(new Category("Tin Tức", "http://www.voatiengviet.com/api/zkvypemovm"));
            Categories.Add(new Category("Việt Nam", "http://www.voatiengviet.com/api/z$uyietpv_"));
            Categories.Add(new Category("Thế giới", "http://www.voatiengviet.com/api/z_ty_erivy"));
            Categories.Add(new Category("Kinh tế", "http://www.voatiengviet.com/api/zquyvekivr"));
            Categories.Add(new Category("Nghệ thuật - giải trí", "http://www.voatiengviet.com/api/zguyre_pvo"));
            Categories.Add(new Category("Sức khỏe", "http://www.voatiengviet.com/api/z-tymevivy"));
            Categories.Add(new Category("Đời sống", "http://www.voatiengviet.com/api/zouytegivq"));
            Categories.Add(new Category("Giáo dục", "http://www.voatiengviet.com/api/ziuyrejivo"));
            Categories.Add(new Category("Phụ nữ", "http://www.voatiengviet.com/api/zrkypeupvy"));
            Categories.Add(new Category("Khoa học công nghệ", "http://www.voatiengviet.com/api/zkuytempvq"));
            Categories.Add(new Category("Thể thao", "http://www.voatiengviet.com/api/zjuyqeypvi"));
            Categories.Add(new Category("Người Việt hải ngoại", "http://www.voatiengviet.com/api/zruyyeuivt"));
            Categories.Add(new Category("Châu Á", "http://www.voatiengviet.com/api/z$qvvetkvr"));
            Categories.Add(new Category("Châu Âu", "http://www.voatiengviet.com/api/zmuyoe$ivm"));
            Categories.Add(new Category("Trung Đông", "http://www.voatiengviet.com/api/zvuy_eopvv"));
            Categories.Add(new Category("Châu Phi", "http://www.voatiengviet.com/api/zytyyeqivv"));
            Categories.Add(new Category("Châu Mỹ", "http://www.voatiengviet.com/api/zutyrepivi"));
            Categories.Add(new Category("Hoa Kỳ", "http://www.voatiengviet.com/api/z-uyoevpvm"));
            Categories.Add(new Category("Học tiếng Anh", "http://www.voatiengviet.com/api/zotyqegovi"));
            Categories.Add(new Category("Blog", "http://www.voatiengviet.com/api/zgvyme_ov_"));


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
                    var sub = item.Descendants().Where(e => e.Name == "enclosure").FirstOrDefault();
                    feed.Thumbnail = sub.Attribute("url").Value;
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
