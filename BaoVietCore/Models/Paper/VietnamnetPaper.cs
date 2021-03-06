﻿using BaoVietCore.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Windows.UI.Xaml;
using System.Xml.Linq;
using HtmlAgilityPack;
using System.Net;
using BaoVietCore.Models.XmlParser;

namespace BaoVietCore.Models
{
    public class VietnamnetPaper : PaperBase
    {
        public VietnamnetPaper(PaperType type) : base(type)
        {
            Title = "Vietnamnet";
            HomePage = "http://vietnamnet.vn/";
            ImageSource = "ms-appx:///Assets/Logo/logo-vietnamnet.png";


            Categories.Add(new Category("Trang chủ", "http://vietnamnet.vn/rss/home.rss"));
            Categories.Add(new Category("Xã hội", "http://vietnamnet.vn/rss/xa-hoi.rss"));
            Categories.Add(new Category("Công nghệ", "http://vietnamnet.vn/rss/cong-nghe-thong-tin-vien-thong.rss"));
            Categories.Add(new Category("Kinh doanh", "http://vietnamnet.vn/rss/kinh-doanh.rss"));
            Categories.Add(new Category("Giáo dục", "http://vietnamnet.vn/rss/giao-duc.rss"));
            Categories.Add(new Category("Chính trị", "http://vietnamnet.vn/rss/chinh-tri.rss"));
            Categories.Add(new Category("Giải trí", "http://vietnamnet.vn/rss/giai-tri.rss"));
            Categories.Add(new Category("Sức khỏe", "http://vietnamnet.vn/rss/suc-khoe.rss"));
            Categories.Add(new Category("Thể thao", "http://vietnamnet.vn/rss/the-thao.rss"));
            Categories.Add(new Category("Đời sống", "http://vietnamnet.vn/rss/doi-song.rss"));
            Categories.Add(new Category("Quốc tế", "http://vietnamnet.vn/rss/quoc-te.rss"));
            Categories.Add(new Category("Bất động sản", "http://vietnamnet.vn/rss/bat-dong-san.rss"));
            Categories.Add(new Category("Bạn đọc", "http://vietnamnet.vn/rss/ban-doc-phap-luat.rss"));
            Categories.Add(new Category("Tin mới nóng", "http://vietnamnet.vn/rss/moi-nong.rss"));
            Categories.Add(new Category("Tin nổi bật", "http://vietnamnet.vn/rss/tin-noi-bat.rss"));
            Categories.Add(new Category("Tuần Việt Nam", "http://vietnamnet.vn/rss/tuanvietnam.rss"));
            Categories.Add(new Category("Góc nhìn thẳng", "http://vietnamnet.vn/rss/goc-nhin-thang.rss"));

            foreach (var item in Categories)
            {
                item.Owner = this;
            }
        }
        
        
    }
}
