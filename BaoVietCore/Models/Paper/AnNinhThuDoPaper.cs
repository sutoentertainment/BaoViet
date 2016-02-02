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
    public class AnNinhThuDoPaper : PaperBase
    {
        public AnNinhThuDoPaper(PaperType type) : base(type)
        {
            //FrontPagePaper.Add(new VnExpressPaper() { Title = "An ninh thủ đô", Type = PaperType.AnNinhThuDo, HomePage = "http://anninhthudo.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-antd.png" });
            Title = "An ninh thủ đô";
            HomePage = "http://anninhthudo.vn/";
            ImageSource = "ms-appx:///Assets/Logo/logo-antd.png";


            foreach (var item in Categories)
            {
                item.Owner = this;
            }
        }

        
    }
}
