using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using BaoVietCore;

namespace UnitTest
{
    [TestClass]
    public class BaoVietTest
    {
        public Manager Manager { get; set; }
        public BaoVietTest()
        {
            Manager = new Manager();
        }

        [TestMethod]
        public void TestRssSource()
        {
        }

        [TestMethod]
        public void DatabaseInsert()
        {
        }

        [TestMethod]
        public void DatabaseDelete()
        {
        }

        [TestMethod]
        public void AuthenticationHello()
        {
        }

        [TestMethod]
        public void AuthenticationPin()
        {
        }
    }
}
