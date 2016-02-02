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
    public class NhipCauDauTuPaper : PaperBase
    {
        public NhipCauDauTuPaper(PaperType type) : base(type)
        {
            //FrontPagePaper.Add(new VnExpressPaper() { Title = "Nhịp cầu đầu tư", Type = PaperType.NhipCauDauTu, HomePage = "http://nhipcaudautu.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-ncdt.png" });

            Title = "Nhịp cầu đầu tư";
            HomePage = "http://nhipcaudautu.vn/";
            ImageSource = "ms-appx:///Assets/Logo/logo-ncdt.png";


            foreach (var item in Categories)
            {
                item.Owner = this;
            }
        }

        
    }
}
