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
    public class PcWorldPaper : PaperBase
    {
        public PcWorldPaper(PaperType type) : base(type)
        {
            //FrontPagePaper.Add(new VnExpressPaper() { Title = "Pc World VN", Type = PaperType.PcWorld, HomePage = "http://www.pcworld.com.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-pcworld.png" });

            Title = "Pc World VN";
            HomePage = "http://www.pcworld.com.vn/";
            ImageSource = "ms-appx:///Assets/Logo/logo-pcworld.png";


            Categories.Add(new Category("Trang chủ", "http://www.pcworld.com.vn/articles.rss"));
            Categories.Add(new Category("Tin tức", "http://www.pcworld.com.vn/articles/tin-tuc.rss"));
            Categories.Add(new Category("Sản phẩm", "http://www.pcworld.com.vn/articles/san-pham.rss"));
            Categories.Add(new Category("Công nghệ", "http://www.pcworld.com.vn/articles/cong-nghe.rss"));
            Categories.Add(new Category("Kinh Doanh", "http://www.pcworld.com.vn/articles/kinh-doanh.rss"));
            Categories.Add(new Category("Giải trí", "http://www.pcworld.com.vn/articles/game.rss"));
            Categories.Add(new Category("Test Lab", "http://www.pcworld.com.vn/articles/preview.rss"));


            foreach (var item in Categories)
            {
                item.Owner = this;
            }
        }
    }
}
