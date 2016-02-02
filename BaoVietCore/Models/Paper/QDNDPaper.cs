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
    public class QDNDPaper : PaperBase
    {
        public QDNDPaper(PaperType type) : base(type)
        {
            //FrontPagePaper.Add(new VnExpressPaper() { Title = "Quân đội nhân dân", Type = PaperType.QuanDoiNhanDan, HomePage = "http://www.qdnd.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-qdnd.png" });
            Title = "Quân đội nhân dân";
            HomePage = "http://www.qdnd.vn/";
            ImageSource = "ms-appx:///Assets/Logo/logo-qdnd.png";


            foreach (var item in Categories)
            {
                item.Owner = this;
            }
        }

        
    }
}
