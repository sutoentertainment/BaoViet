using BaoVietCore.Interfaces;
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
    public class BBCPaper : PaperBase
    {
        public BBCPaper(PaperType type) : base(type)
        {
            //FrontPagePaper.Add(new VnExpressPaper() { Title = "BBC tiếng Việt", Type = PaperType.BBC, HomePage = "http://www.bbc.com/vietnamese", ImageSource = "ms-appx:///Assets/Logo/logo-bbc.png" });
            Title = "BBC tiếng Việt";
            HomePage = "http://www.bbc.com/vietnamese";
            ImageSource = "ms-appx:///Assets/Logo/logo-bbc.png";


            Categories.Add(new Category("Trang chủ", "http://www.bbc.com/vietnamese/index.xml"));
            Categories.Add(new Category("Việt Nam", "http://www.bbc.com/vietnamese/vietnam/index.xml"));
            Categories.Add(new Category("Thế giới", "http://www.bbc.com/vietnamese/world/index.xml"));
            Categories.Add(new Category("Diễn đàn", "http://www.bbc.com/vietnamese/topics/forum/index.xml"));
            Categories.Add(new Category("Kinh tế", "http://www.bbc.com/vietnamese/business/index.xml"));
            Categories.Add(new Category("Nhịp sống mới", "http://www.bbc.com/vietnamese/topics/magazine/index.xml"));
            Categories.Add(new Category("Thể thao", "http://www.bbc.com/vietnamese/sport/index.xml"));
            Categories.Add(new Category("Học tiếng Anh", "http://www.bbc.com/vietnamese/english/index.xml"));
            Categories.Add(new Category("Hình ảnh", "http://www.bbc.com/vietnamese/topics/photo/index.xml"));
            Categories.Add(new Category("Audio", "http://www.bbc.com/vietnamese/topics/audio/index.xml"));
            Categories.Add(new Category("Video", "http://www.bbc.com/vietnamese/topics/video/index.xml"));


            foreach (var item in Categories)
            {
                item.Owner = this;
            }
        }
    }
}
