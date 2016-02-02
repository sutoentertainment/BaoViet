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
    public class GameThuPaper : PaperBase
    {
        public GameThuPaper(PaperType type) : base(type)
        {
            //FrontPagePaper.Add(new VnExpressPaper() { Title = "Game thủ", Type = PaperType.Gamethu, HomePage = "http://gamethu.vnexpress.net/", ImageSource = "ms-appx:///Assets/Logo/logo-gamethu.png" });
            Title = "Game thủ";
            HomePage = "http://gamethu.vnexpress.net/";
            ImageSource = "ms-appx:///Assets/Logo/logo-gamethu.png";


            Categories.Add(new Category("Trang chủ", "http://gamethu.net/rss/tin-moi-nhat.rss"));
            Categories.Add(new Category("eSport", "http://gamethu.net/rss/esport.rss"));
            Categories.Add(new Category("Mobile", "http://gamethu.net/rss/mobile.rss"));
            Categories.Add(new Category("Làng Game", "http://gamethu.net/rss/lang-game.rss"));
            Categories.Add(new Category("Online", "http://gamethu.net/rss/online.rss"));
            Categories.Add(new Category("Offline", "http://gamethu.net/rss/offline.rss"));


            foreach (var item in Categories)
            {
                item.Owner = this;
            }
        }

        
    }
}
