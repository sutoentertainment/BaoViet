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
    public class WebTreThoPaper : PaperBase
    {
        public WebTreThoPaper(PaperType type) : base(type)
        {
            //FrontPagePaper.Add(new VnExpressPaper() { Title = "Web trẻ thơ", Type = PaperType.WebTreTho, HomePage = "http://www.webtretho.com/", ImageSource = "ms-appx:///Assets/Logo/logo-webtretho.png" });
            Title = "Web trẻ thơ";
            HomePage = "http://www.webtretho.com/";
            ImageSource = "ms-appx:///Assets/Logo/logo-webtretho.png";


            Categories.Add(new Category("Trang chủ", "http://www.webtretho.com/forum/external.php?type=RSS2"));


            foreach (var item in Categories)
            {
                item.Owner = this;
            }
        }

        
    }
}
