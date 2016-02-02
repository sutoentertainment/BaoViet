using HtmlAgilityPack;
using BaoVietCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BaoVietCore.Models.Paper
{
    public class TinTheThaoPaper : PaperBase
    {
        public TinTheThaoPaper(PaperType type) : base(type)
        {
            //FrontPagePaper.Add(new VnExpressPaper() { Title = "Tin thể thao", Type = PaperType.TinTheThao, HomePage = "http://www.tinthethao.com.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-tinthethao.png" });
            Title = "Tin thể thao";
            HomePage = "http://www.tinthethao.com.vn/";
            ImageSource = "ms-appx:///Assets/Logo/logo-tinthethao.png";


            Categories.Add(new Category("Tin mới nhất", "http://feeds.feedburner.com/com/KYlp"));
            Categories.Add(new Category("Quần Vợt", "http://feeds.feedburner.com/com/dfieq"));
            Categories.Add(new Category("Ôtô-Xe máy", "http://feeds.feedburner.com/com/offiwi"));


            foreach (var item in Categories)
            {
                item.Owner = this;
            }
        }
    }
}
