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
    public class GenkPaper : PaperBase
    {
        public GenkPaper(PaperType type) : base(type)
        {
            //FrontPagePaper.Add(new VnExpressPaper() { Title = "Genk", Type = PaperType.Genk, HomePage = "http://genk.vn", ImageSource = "ms-appx:///Assets/Logo/logo-genk.png" });
            Title = "Genk";
            HomePage = "http://genk.vn";
            ImageSource = "ms-appx:///Assets/Logo/logo-genk.png";
        }

        
    }
}
