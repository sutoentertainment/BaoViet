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
    public class TheGioiGamePaper : PaperBase
    {
        public TheGioiGamePaper(PaperType type) : base(type)
        {
            //FrontPagePaper.Add(new VnExpressPaper() { Title = "Thế giới Game", Type = PaperType.TheGioiGame, HomePage = "http://thegioigame.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-thegioigame.png" });
            Title = "Thế giới Game";
            HomePage = "http://thegioigame.vn/";
            ImageSource = "ms-appx:///Assets/Logo/logo-thegioigame.png";


            Categories.Add(new Category("Trang chủ", "http://thegioigame.vn/feed"));


            foreach (var item in Categories)
            {
                item.Owner = this;
            }
        }

        
    }
}
