using HtmlAgilityPack;
using BaoVietCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BaoVietCore.Models.XmlParser;

namespace BaoVietCore.Models.Paper
{
    public class VietnamPlusPaper : PaperBase
    {
        public VietnamPlusPaper(PaperType type) : base(type)
        {
            //FrontPagePaper.Add(new VnExpressPaper() { Title = "VietnamPlus", Type = PaperType.VietnamPlus, HomePage = "http://www.vietnamplus.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-vnp.png" });
            Title = "VietnamPlus";
            HomePage = "http://www.vietnamplus.vn/";
            ImageSource = "ms-appx:///Assets/Logo/logo-vnp.png";


            Categories.Add(new Category("Trang chủ", "http://www.vietnamplus.vn/rss/news.rss"));
            Categories.Add(new Category("Kinh tế", "http://www.vietnamplus.vn/rss/kinhte.rss"));
            Categories.Add(new Category("Chính trị", "http://www.vietnamplus.vn/rss/chinhtri.rss"));
            Categories.Add(new Category("Xã hội", "http://www.vietnamplus.vn/rss/xahoi.rss"));
            Categories.Add(new Category("Thế giới", "http://www.vietnamplus.vn/rss/thegioi.rss"));
            Categories.Add(new Category("Đời sống", "http://www.vietnamplus.vn/rss/doisong.rss"));
            Categories.Add(new Category("Văn hóa", "http://www.vietnamplus.vn/rss/vanhoa.rss"));
            Categories.Add(new Category("Thể thao", "http://www.vietnamplus.vn/rss/thethao.rss"));
            Categories.Add(new Category("Khoa học", "http://www.vietnamplus.vn/rss/khoahoc.rss"));
            Categories.Add(new Category("Công nghệ", "http://www.vietnamplus.vn/rss/congnghe.rss"));
            Categories.Add(new Category("Chuyện lạ", "http://www.vietnamplus.vn/rss/chuyenla.rss"));
            Categories.Add(new Category("Rao vặt", "http://www.vietnamplus.vn/rss/raovat.rss"));
            


            foreach (var item in Categories)
            {
                item.Owner = this;
            }
        }

        
    }
}
