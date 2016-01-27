using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using BaoVietCore;
using BaoVietCore.Models;
using System.Linq;
using BaoVietCore.Interfaces;

namespace UnitTest
{
    [TestClass]
    public class BaoVietTest
    {
        public Manager Manager { get; set; }
        public BaoVietTest()
        {
            Manager = new Manager();
            Manager.Database.CreateTable<FeedItem>();
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void TestRssSource()
        {
        }

        [TestMethod]
        public void DatabaseInsert()
        {
            var number_of_feed_before = Manager.Database.GetItems<FeedItem>().Count();
            var feed = new FeedItem();
            Manager.Database.AddItem(feed);
            var number_of_feed_after = Manager.Database.GetItems<FeedItem>().Count();

            Assert.AreEqual(number_of_feed_before + 1, number_of_feed_after);
        }

        [TestMethod]
        public void DatabaseDelete()
        {
            var feed = new FeedItem();
            Manager.Database.AddItem(feed);
            var number_of_feed_before = Manager.Database.GetItems<FeedItem>().Count();

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
