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
    public class ThongTinCongNghePaper : PaperBase
    {
        public ThongTinCongNghePaper(PaperType type) : base(type)
        {
            //FrontPagePaper.Add(new VnExpressPaper() { Title = "Thông tin công nghệ", Type = PaperType.ThongTinCongNghe, HomePage = "http://www.thongtincongnghe.com/", ImageSource = "ms-appx:///Assets/Logo/logo-ttcn.png" });

            Title = "Thông tin công nghệ";
            HomePage = "http://www.thongtincongnghe.com/";
            ImageSource = "ms-appx:///Assets/Logo/logo-ttcn.png";


            Categories.Add(new Category("Trang chủ", "http://feeds.thongtincongnghe.com/ttcn"));


            foreach (var item in Categories)
            {
                item.Owner = this;
            }
        }

        
    }
}
