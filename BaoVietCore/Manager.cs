using BaoVietCore.Helpers;
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
        public IIAPService IAPService { get; set; }
        public IKeyboardService KeyboardService { get; set; }
        public IWebService WebService { get; set; }
        public IWebService MercuryService { get; set; }
        public ILocalizationService LocalizationService { get; set; }
        public ILogService LogService { get; set; }
        public IAuthenticationService AuthenticationService { get; set; }
        public IRateUsService RateUsService { get; set; }
        public IDatabase Database { get; set; }
        public IImageService ImageService { get; set; }
        public ITrackingService TrackingService { get; set; }
        public IRssSource RssService { get; set; }
        public DeviceService DeviceService { get; set; }
        public ICameraService CameraService { get; set; }
        public ISettingsService SettingsService { get; set; }
        public IMarkDownParser MarkDownService { get; set; }
        public StorageService StorageService { get; set; }

        public Manager()
        {
            Current = this;

            StorageService = new StorageService(this);
            WebService = new WebService(this);
            MercuryService = new MercuryClient(this, "LDlCrO7jtLcMyCkscinCW7tKMxC7VwRVMPFpvBhd");
            Database = new SqliteNetDatabase(this);
            AuthenticationService = new AuthenticationService(this);
            RateUsService = new RateUsService(this);
            ImageService = new ImageService(this);
            TrackingService = new LocalyticsAdapterService(this);
            RssService = new RssService(this);
            IAPService = new IAPService(this);
            CameraService = new BasicCameraService(this);
            SettingsService = new SettingsService(this);
            KeyboardService = new KeyboardService(this);
            MarkDownService = new MarkDownService(this);
            LogService = new LogService(this, StorageService);
        }
        public void Configure()
        {
            //KeyboardService = new KeyboardService(this);
            //LocalizationService = new LocalizationService(this);
        }
    }
}
