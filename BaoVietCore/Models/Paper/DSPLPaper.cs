﻿using BaoVietCore.Interfaces;
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
    public class DSPLPaper : PaperBase
    {
        public DSPLPaper(PaperType type) : base(type)
        {
            Title = "Dân trí";
            HomePage = "http://dantri.com.vn";
            ImageSource = "ms-appx:///Assets/Logo/logo-dantri.png";


            Categories.Add(new Category("Trang chủ", "http://dantri.com.vn/trangchu.rss"));
            Categories.Add(new Category("Sức khỏe", "http://dantri.com.vn/suc-khoe.rss"));
            Categories.Add(new Category("Xã hội", "http://dantri.com.vn/xa-hoi.rss"));
            Categories.Add(new Category("Giải trí", "http://dantri.com.vn/giai-tri.rss"));
            Categories.Add(new Category("Giáo dục - Khuyến học", "http://dantri.com.vn/giao-duc-khuyen-hoc.rss"));
            Categories.Add(new Category("Thể thao", "http://dantri.com.vn/the-thao.rss"));
            Categories.Add(new Category("Thế giới", "http://dantri.com.vn/the-gioi.rss"));
            Categories.Add(new Category("Kinh doanh", "http://dantri.com.vn/kinh-doanh.rss"));
            Categories.Add(new Category("Ô tô - Xe máy", "http://dantri.com.vn/o-to-xe-may.rss"));
            Categories.Add(new Category("Sức mạnh số", "http://dantri.com.vn/suc-manh-so.rss"));
            Categories.Add(new Category("Tình yêu - Giới tính", "http://dantri.com.vn/tinh-yeu-gioi-tinh.rss"));
            Categories.Add(new Category("Chuyện lạ", "http://dantri.com.vn/chuyen-la.rss"));
            Categories.Add(new Category("Việc làm", "http://dantri.com.vn/viec-lam.rss"));
            Categories.Add(new Category("Nhịp sống trẻ", "http://dantri.com.vn/nhip-song-tre.rss"));
            Categories.Add(new Category("Tấm lòng nhân ái", "http://dantri.com.vn/tam-long-nhan-ai.rss"));
            Categories.Add(new Category("Pháp luật", "http://dantri.com.vn/phap-luat.rss"));
            Categories.Add(new Category("Bạn đọc", "http://dantri.com.vn/ban-doc.rss"));
            Categories.Add(new Category("Diễn đàn", "http://dantri.com.vn/dien-dan.rss"));
            Categories.Add(new Category("Blog", "http://dantri.com.vn/blog.rss"));
            Categories.Add(new Category("Văn hóa", "http://dantri.com.vn/van-hoa.rss"));
            Categories.Add(new Category("Du học", "http://dantri.com.vn/du-hoc.rss"));
            Categories.Add(new Category("Đời sống", "http://dantri.com.vn/doi-song.rss"));


            foreach (var item in Categories)
            {
                item.Owner = this;
            }
        }

        
    }
}
