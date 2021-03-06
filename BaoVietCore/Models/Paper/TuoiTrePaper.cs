﻿using HtmlAgilityPack;
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
    public class TuoiTrePaper : PaperBase
    {
        public TuoiTrePaper(PaperType type) : base(type)
        {
            //FrontPagePaper.Add(new VnExpressPaper() { Title = "Tuổi trẻ", Type = PaperType.TuổiTre, HomePage = "http://tuoitre.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-tuoitre.png" });
            Title = "Tuổi trẻ";
            HomePage = "http://tuoitre.vn/";
            ImageSource = "ms-appx:///Assets/Logo/logo-tuoitre.png";


            Categories.Add(new Category("Trang chủ", "http://tuoitre.vn/rss/tt-tin-moi-nhat.rss"));
            Categories.Add(new Category("Chính trị - Xã hội", "http://tuoitre.vn/rss/tt-chinh-tri-xa-hoi.rss"));
            Categories.Add(new Category("Thế giới", "http://tuoitre.vn/rss/tt-the-gioi.rss"));
            Categories.Add(new Category("Pháp luật", "http://tuoitre.vn/rss/tt-phap-luat.rss"));
            Categories.Add(new Category("Kinh tế", "http://tuoitre.vn/rss/tt-kinh-te.rss"));
            Categories.Add(new Category("Sống khỏe", "http://tuoitre.vn/rss/tt-song-khoe.rss"));
            Categories.Add(new Category("Giáo dục", "http://tuoitre.vn/rss/tt-giao-duc.rss"));
            Categories.Add(new Category("Thể thao", "http://tuoitre.vn/rss/tt-the-thao.rss"));
            Categories.Add(new Category("Văn hóa - Giải trí", "http://tuoitre.vn/rss/tt-van-hoa-giai-tri.rss"));
            Categories.Add(new Category("Nhịp sống trẻ", "http://tuoitre.vn/rss/tt-nhip-song-tre.rss"));
            Categories.Add(new Category("Nhịp sống số", "http://tuoitre.vn/rss/tt-nhip-song-so.rss"));
            Categories.Add(new Category("Bạn đọc", "http://tuoitre.vn/rss/tt-ban-doc.rss"));
            Categories.Add(new Category("Du lịch", "http://tuoitre.vn/rss/tt-du-lich.rss"));


            foreach (var item in Categories)
            {
                item.Owner = this;
            }
        }
    }
}
