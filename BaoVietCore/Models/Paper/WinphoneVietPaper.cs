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
    public class WinphoneVietPaper : PaperBase
    {
        public WinphoneVietPaper(PaperType type) : base(type)
        {
            //FrontPagePaper.Add(new VnExpressPaper() { Title = "WinphoneViet", Type = PaperType.WinphoneViet, HomePage = "http://winphoneviet.com", ImageSource = "ms-appx:///Assets/Logo/logo-wpv.png" });
            Title = "WinphoneViet";
            HomePage = "http://winphoneviet.com";
            ImageSource = "ms-appx:///Assets/Logo/logo-wpv.png";


            Categories.Add(new Category("Trang chủ", "http://www.winphoneviet.com/feed/"));


            foreach (var item in Categories)
            {
                item.Owner = this;
            }
        }

        
    }
}
