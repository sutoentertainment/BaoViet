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
    public class QuanTriMangPaper : PaperBase
    {
        public QuanTriMangPaper(PaperType type) : base(type)
        {
            //FrontPagePaper.Add(new VnExpressPaper() { Title = "Quản trị mạng", Type = PaperType.QuanTriMang, HomePage = "http://quantrimang.com/", ImageSource = "ms-appx:///Assets/Logo/logo-quantrimang.png" });
            Title = "Quản trị mạng";
            HomePage = "http://quantrimang.com/";
            ImageSource = "ms-appx:///Assets/Logo/logo-quantrimang.png";


            Categories.Add(new Category("Trang chủ", "http://quantrimang.com/rss"));


            foreach (var item in Categories)
            {
                item.Owner = this;
            }
        }
    }
}
