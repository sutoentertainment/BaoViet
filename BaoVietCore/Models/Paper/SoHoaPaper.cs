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
    public class SoHoaPaper : PaperBase
    {
        public SoHoaPaper(PaperType type) : base(type)
        {
            //FrontPagePaper.Add(new VnExpressPaper() { Title = "Số hóa", Type = PaperType.SốHóa, HomePage = "http://sohoa.vnexpress.net", ImageSource = "ms-appx:///Assets/Logo/logo-sohoa.png" });
            Title = "Số hóa";
            HomePage = "http://sohoa.vnexpress.net";
            ImageSource = "ms-appx:///Assets/Logo/logo-sohoa.png";


            Categories.Add(new Category("Trang chủ", "http://vnexpress.net/rss/so-hoa.rss"));
            


            foreach (var item in Categories)
            {
                item.Owner = this;
            }
        }

        
    }
}
