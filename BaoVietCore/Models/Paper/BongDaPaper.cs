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
    public class BongDaPaper : PaperBase
    {
        public BongDaPaper(PaperType type) : base(type)
        {
            //FrontPagePaper.Add(new VnExpressPaper() { Title = "Bóng đá", Type = PaperType.BongDa, HomePage = "http://www.bongda.com.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-bongda.png" });

            Title = "Bóng đá";
            HomePage = "http://www.bongda.com.vn";
            ImageSource = "ms-appx:///Assets/Logo/logo-bongda.png";


            Categories.Add(new Category("Trang chủ", "http://www.bongda.com.vn/feed/"));


            foreach (var item in Categories)
            {
                item.Owner = this;
            }
        }

        
    }
}
