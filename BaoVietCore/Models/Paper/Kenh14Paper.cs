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
    public class Kenh14Paper : PaperBase
    {
        public Kenh14Paper(PaperType type) : base(type)
        {
            //FrontPagePaper.Add(new VnExpressPaper() { Title = "Kênh 14", Type = PaperType.Kenh14, HomePage = "http://kenh14.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-kenh14.png" });
            Title = "Kênh 14";
            HomePage = "http://kenh14.vn/";
            ImageSource = "ms-appx:///Assets/Logo/logo-kenh14.png";


            Categories.Add(new Category("Trang chủ", "http://kenh14.vn/home.rss"));
            Categories.Add(new Category("Star", "http://kenh14.vn/star.rss"));
            Categories.Add(new Category("Musik", "http://kenh14.vn/musik.rss"));
            Categories.Add(new Category("Cine", "http://kenh14.vn/cine.rss"));
            Categories.Add(new Category("Tv Show", "http://kenh14.vn/tv-show.rss"));
            Categories.Add(new Category("Fashion", "http://kenh14.vn/fashion.rss"));
            Categories.Add(new Category("Đời sống", "http://kenh14.vn/doi-song.rss"));
            Categories.Add(new Category("Xã hội", "http://kenh14.vn/xa-hoi.rss"));
            Categories.Add(new Category("Thế giới", "http://kenh14.vn/the-gioi.rss"));
            Categories.Add(new Category("Sức khỏe giới tính", "http://kenh14.vn/suc-khoe-gioi-tinh.rss"));
            Categories.Add(new Category("Made by me", "http://kenh14.vn/made-by-me.rss"));
            Categories.Add(new Category("Sport", "http://kenh14.vn/sport.rss"));
            Categories.Add(new Category("Khám phá", "http://kenh14.vn/kham-pha.rss"));
            Categories.Add(new Category("2-Tek", "http://kenh14.vn/2-tek.rss"));
            Categories.Add(new Category("Lạ & cool", "http://kenh14.vn/la-cool.rss"));
            Categories.Add(new Category("Học đường", "http://kenh14.vn/hoc-duong.rss"));
            Categories.Add(new Category("Video", "http://kenh14.vn/video.rss"));


            foreach (var item in Categories)
            {
                item.Owner = this;
            }
        }

        
    }
}
