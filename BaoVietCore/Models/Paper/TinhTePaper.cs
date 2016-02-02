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
    public class TinhTePaper : PaperBase
    {
        public TinhTePaper(PaperType type) : base(type)
        {
            //FrontPagePaper.Add(new VnExpressPaper() { Title = "Tinh tế", Type = PaperType.TinhTế, HomePage = "http://tinhte.vn", ImageSource = "ms-appx:///Assets/Logo/logo-tinhte.png" });
            Title = "Tinh tế";
            HomePage = "http://tinhte.vn";
            ImageSource = "ms-appx:///Assets/Logo/logo-tinhte.png";


            Categories.Add(new Category("Trang chủ", "https://tinhte.vn/rss"));


            foreach (var item in Categories)
            {
                item.Owner = this;
            }
        }

        
    }
}
