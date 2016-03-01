using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using BaoVietCore;
using BaoVietCore.Models;
using System.Linq;
using BaoVietCore.Interfaces;
using BaoVietCore.Services;
using BaoVietCore.Factory;
using System.Collections.Generic;
using BaoVietCore.Helpers;
using System.Diagnostics;

namespace UnitTest
{
    [TestClass]
    public class BaoVietTest
    {
        public Manager Manager { get; set; }
        public List<IPaper> FrontPagePaper { get; private set; }

        public BaoVietTest()
        {
            Manager = new Manager();
            IStateCondition condiction = new StateCondition(720);
            Manager.Current.DeviceService = new DeviceService(Manager, condiction);
            Manager.Database.CreateTable<FeedItem>();
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void TestRssSource()
        {
            LoadData();
            foreach (var paper in FrontPagePaper)
            {
                foreach (var category in paper.Categories)
                {
                    var xmlParser = XmlParserFactory.Create(category.Owner.Type);
                    var xml = Manager.WebService.GetString(category.Source);
                    xml.Wait();
                    xmlParser.Load(xml.Result);
                    var feeds = xmlParser.GetFeed();
                    if (feeds.Count() > 0)
                        Debug.WriteLine("Ok");
                    else
                        Debug.WriteLine("Fail");
                }
            }
        }

        public void LoadData()
        {
            FrontPagePaper = new List<IPaper>();
            //FrontPagePaper.Add(PaperFactory.Create(PaperType.VnExpress));
            //FrontPagePaper.Add(PaperFactory.Create(PaperType.DânTrí));
            //FrontPagePaper.Add(PaperFactory.Create(PaperType.TienPhong));
            //FrontPagePaper.Add(PaperFactory.Create(PaperType.VietnamPlus));
            //FrontPagePaper.Add(PaperFactory.Create(PaperType.TinhTế));
            //FrontPagePaper.Add(PaperFactory.Create(PaperType.SốHóa));
            //FrontPagePaper.Add(PaperFactory.Create(PaperType.Vietnamnet));
            //FrontPagePaper.Add(PaperFactory.Create(PaperType.WinphoneViet));
            //FrontPagePaper.Add(PaperFactory.Create(PaperType.BaoMoi));
            //FrontPagePaper.Add(PaperFactory.Create(PaperType.NgôiSao));
            //FrontPagePaper.Add(PaperFactory.Create(PaperType.TuổiTre));
            //FrontPagePaper.Add(PaperFactory.Create(PaperType.Gamethu));
            //FrontPagePaper.Add(PaperFactory.Create(PaperType.Genk));
            //FrontPagePaper.Add(PaperFactory.Create(PaperType.LaoDong));
            //FrontPagePaper.Add(PaperFactory.Create(PaperType.BongDa));
            //FrontPagePaper.Add(PaperFactory.Create(PaperType.TinTheThao));
            //FrontPagePaper.Add(PaperFactory.Create(PaperType.Báo24H));
            //FrontPagePaper.Add(PaperFactory.Create(PaperType.PcWorld));
            //FrontPagePaper.Add(PaperFactory.Create(PaperType.TheGioiGame));
            //FrontPagePaper.Add(PaperFactory.Create(PaperType.ThongTinCongNghe));
            //FrontPagePaper.Add(PaperFactory.Create(PaperType.QuanTriMang));
            //FrontPagePaper.Add(PaperFactory.Create(PaperType.Zing));
            //FrontPagePaper.Add(PaperFactory.Create(PaperType.GiaDinh));
            //FrontPagePaper.Add(PaperFactory.Create(PaperType.ICTNews));
            //FrontPagePaper.Add(PaperFactory.Create(PaperType.VnEconomy));
            //FrontPagePaper.Add(PaperFactory.Create(PaperType.NhipCauDauTu));
            //FrontPagePaper.Add(PaperFactory.Create(PaperType.BBC));
            //FrontPagePaper.Add(PaperFactory.Create(PaperType.VOA));
            //FrontPagePaper.Add(PaperFactory.Create(PaperType.Eva));
            //FrontPagePaper.Add(PaperFactory.Create(PaperType.CongAnNhanDan));
            //FrontPagePaper.Add(PaperFactory.Create(PaperType.AnNinhThuDo));
            //FrontPagePaper.Add(PaperFactory.Create(PaperType.QuanDoiNhanDan));


            //FrontPagePaper.Add(PaperFactory.Create(PaperType.DatViet));
            //FrontPagePaper.Add(PaperFactory.Create(PaperType.WebTreTho));
            //FrontPagePaper.Add(PaperFactory.Create(PaperType.VietBao));
            //FrontPagePaper.Add(PaperFactory.Create(PaperType.Kenh14));



            //FrontPagePaper.Add(new VnExpressPaper() { Title = "Petro Times", Type = PaperType.PetroTimes, HomePage = "http://petrotimes.vn", ImageSource = "ms-appx:///Assets/Logo/logo-petro.png" });
            //FrontPagePaper.Add(new VnExpressPaper() { Title = "Đời sống pháp luật", Type = PaperType.DoiSongPhapLuat, HomePage = "http://www.doisongphapluat.com/", ImageSource = "ms-appx:///Assets/Logo/logo-dspl.png" });
        }


        [TestMethod]
        public void DatabaseAddDelete()
        {
            var feed = new FeedItem();
            Manager.Database.AddItem(feed);
            var number_of_feed_before = Manager.Database.GetItems<FeedItem>().Count();

            Assert.AreEqual(number_of_feed_before + 1, number_of_feed_before);

            Manager.Database.Delete(feed);
            var number_of_feed_after = Manager.Database.GetItems<FeedItem>().Count();

            Assert.AreEqual(number_of_feed_before - 1, number_of_feed_after);
        }

        [TestMethod]
        public void AuthenticationHello()
        {

        }

        [TestMethod]
        public void AuthenticationPin()
        {
            Manager.AuthenticationService.DeleteAccount();
            var password = Guid.NewGuid().ToString();

            var result = Manager.AuthenticationService.PasswordAuthen(password);
            Assert.AreEqual(AuthenticationResult.NotAvailable, result);


            Manager.AuthenticationService.CreatePassword(password);
            result = Manager.AuthenticationService.PasswordAuthen(password);

            Assert.AreEqual(AuthenticationResult.Success, result);

            result = Manager.AuthenticationService.PasswordAuthen(password + "fail");
            Assert.AreEqual(AuthenticationResult.Fail, result);
        }
    }
}
