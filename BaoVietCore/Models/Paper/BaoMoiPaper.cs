using BaoVietCore.Interfaces;
using BaoVietCore.Models.XmlParser;
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

        
    }
}
