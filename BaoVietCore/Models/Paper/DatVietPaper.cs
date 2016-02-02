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
    public class DatVietPaper : PaperBase
    {
        public DatVietPaper(PaperType type) : base(type)
        {
            //FrontPagePaper.Add(new VnExpressPaper() { Title = "Đất Việt", Type = PaperType.DatViet, HomePage = "http://baodatviet.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-datviet.png" });
            Title = "Đất Việt";
            HomePage = "http://baodatviet.vn/";
            ImageSource = "ms-appx:///Assets/Logo/logo-datviet.png";


            foreach (var item in Categories)
            {
                item.Owner = this;
            }
        }

        
    }
}
