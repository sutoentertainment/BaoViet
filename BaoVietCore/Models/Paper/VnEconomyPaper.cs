﻿using HtmlAgilityPack;
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
    public class VnEconomyPaper : PaperBase
    {
        public VnEconomyPaper(PaperType type) : base(type)
        {

            //FrontPagePaper.Add(new VnExpressPaper() { Title = "VnEconomy", Type = PaperType.VnEconomy, HomePage = "http://vneconomy.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-vneconomy.png" });

            Title = "VnEconomy";
            HomePage = "http://vneconomy.vn/";
            ImageSource = "ms-appx:///Assets/Logo/logo-vneconomy.png";


            Categories.Add(new Category("Thời sự", "http://vneconomy.vn/rss/thoi-su.rss"));
            Categories.Add(new Category("Doanh nhân", "http://vneconomy.vn/rss/doanh-nhan.rss"));
            Categories.Add(new Category("Tài chính", "http://vneconomy.vn/rss/tai-chinh.rss"));
            Categories.Add(new Category("Chứng khoán", "http://vneconomy.vn/rss/chung-khoan.rss"));
            Categories.Add(new Category("Thị trường", "http://vneconomy.vn/rss/thi-truong.rss"));
            Categories.Add(new Category("Bất động sản", "http://vneconomy.vn/rss/bat-dong-san.rss"));
            Categories.Add(new Category("Thế giới", "http://vneconomy.vn/rss/the-gioi.rss"));
            Categories.Add(new Category("Cuộc sống số", "http://vneconomy.vn/rss/cuoc-song-so.rss"));
            Categories.Add(new Category("Xe 360°", "http://vneconomy.vn/rss/xe-360.rss"));


            foreach (var item in Categories)
            {
                item.Owner = this;
            }
        }

        
    }
}
