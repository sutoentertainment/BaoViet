using BaoVietCore.Interfaces;
using BaoVietCore.Models;
using BaoVietCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoVietCore
{
    public class Manager
    {
        public static Manager Current { get; set; }

        public IKeyboardService KeyboardService { get; set; }

        public IWebService WebService { get; set; }

        public LocalizationService LocalizationService { get; set; }

        public LogService LogService { get; set; }

        public IDatabase Database { get; set; }

        public Manager()
        {
            Current = this;
            WebService = new WebService(this);
            LogService = new LogService(this);
            Database = new Database(this);

            Database.CreateTable<FeedItem>();
        }
        public void Configure()
        {
            KeyboardService = new KeyboardService(this);
            //LocalizationService = new LocalizationService(this);
        }
    }
}
