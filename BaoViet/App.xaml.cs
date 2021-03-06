﻿using BaoViet.Views;
using System;
using System.Diagnostics;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Networking.Connectivity;
using Windows.Phone.UI.Input;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using BaoViet.DataContext;
using BaoVietCore.Helpers;
using BaoVietCore;
using BaoViet.Interfaces;
using BaoViet.Helpers;
using BaoViet.Services;
using BaoViet.ViewModels;
using Windows.UI.Popups;
using BaoVietCore.Models;
using BaoVietCore.Services;
using GalaSoft.MvvmLight.Threading;
using Microsoft.HockeyApp;
using BaoVietCore.Interfaces;
using System.Collections.Generic;
using BaoViet.Localytics;
using BaoViet.Services.EntityFramework;
using System.Linq;

// The Blank Application template is documented at http://go.microsoft.com/fwlink/?LinkId=402347&clcid=0x409

namespace BaoViet
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    partial class App : ApplicationBase
    {

        public bool CustomBackPressed = false;
        /// <summary>
        /// Allows tracking page views, exceptions and other telemetry through the Microsoft Application Insights service.
        /// </summary>
        //public static Microsoft.ApplicationInsights.TelemetryClient TelemetryClient;


        public new static App Current { get; set; }
        public Frame MasterFrame { get; set; }
        public RootDataContext RootDataContext { get; set; }
        public TileManager TileManager { get; set; }
        public NavigationService NavigationService { get; internal set; }



        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App() : base()
        {
            //TelemetryClient = new Microsoft.ApplicationInsights.TelemetryClient();

            this.InitializeComponent();
            Suspending += OnSuspending;
            Resuming += App_Resuming;
            UnhandledException += App_UnhandledException;
            Current = this;

            HockeyClient.Current.Configure("c393580460234e8887897e0c6caef3d5");
            Manager = new Manager();
            Manager.Database.Configurate();
            // Migration to entity framework
            List<FeedItem> oldFeeds = new List<FeedItem>();
            List<CustomPaper> oldPapers = new List<CustomPaper>();
            if (Manager.Database.GetFeedItem().Count() > 0)
            {
                oldFeeds.AddRange(Manager.Database.GetFeedItem());
                oldFeeds.ForEach(f => { Manager.Database.DeleteFeed(f); });
            }
            if (Manager.Database.GetCustomPaper().Count() > 0)
            {
                oldPapers.AddRange(Manager.Database.GetCustomPaper());
                oldPapers.ForEach(p => { Manager.Database.DeletePaper(p); });
            }
            Manager.Database = new EntityFrameworkContext();
            Manager.Database.Configurate();
            //(Manager.Database as EntityFrameworkContext).Feeds.RemoveRange(ee.Feeds);
            //(Manager.Database as EntityFrameworkContext).Papers.RemoveRange(ee.Papers);
            //(Manager.Database as EntityFrameworkContext).SaveChanges();
            try
            {
                oldFeeds.ForEach(f => { Manager.Database.AddFeedItem(f); });
                oldPapers.ForEach(p => { Manager.Database.AddPaper(p); });
            }
            catch
            {

            }
            RootDataContext = new RootDataContext();
            TileManager = new TileManager();
        }

        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            if (CustomBackPressed)
                return;
            Frame frame = Window.Current.Content as Frame;
            if (frame == null)
            {
                return;
            }

            if (frame.CanGoBack)
            {
                frame.GoBack();
                e.Handled = true;
            }
        }

        private void App_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e != null)
            {
                Exception exception = e.Exception;
                if (exception is NullReferenceException && exception.ToString().ToUpper().Contains("SOMA"))
                {
                    Debug.WriteLine("Handled Smaato null reference exception {0}", exception);
                    e.Handled = true;
                    return;
                }
            }
            // APP SPECIFIC HANDLING HERE

            if (Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                Debugger.Break();
            }

            //TelemetryClient.TrackException(e.Exception);
            Manager.LogService.Log(e.ToString());
            Manager.LogService.Log(e.Message);
            Manager.LogService.Log(e.Exception.ToString());
            Manager.LogService.Log(e.Exception.StackTrace);
            Manager.LogService.Log(e.Exception.Message);

            //Because we dont have time to write the log down, we need to wait to the next time user open app to write it.
            //await Manager.LogService.WriteLog();
            Manager.SettingsService.SetValueLocal("last-log", Manager.LogService.LogText);
        }


        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override async void OnLaunched(LaunchActivatedEventArgs e)
        {
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif

            Frame rootFrame = Window.Current.Content as Frame;
            // Register to network changed event
            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                Manager.TrackingService.AutoIntegrate();
                Manager.IAPService.Init();
                AdDuplex.AdDuplexClient.Initialize("b1169327-404c-4c1f-bc89-45d21f9e9c64");
                DispatcherHelper.Initialize();

                var titleBar = ApplicationView.GetForCurrentView().TitleBar;
                //CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;

                //ApplicationView.GetForCurrentView().Title = "Báo Việt 10";
                titleBar.BackgroundColor = titleBar.ButtonBackgroundColor = (Application.Current.Resources["AppColor"] as SolidColorBrush).Color;
                titleBar.ForegroundColor = titleBar.ButtonForegroundColor = Colors.White;

                //TODO: Setup status bar for phone
                if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
                {
                    StatusBar bar = Windows.UI.ViewManagement.StatusBar.GetForCurrentView();
                    await bar.HideAsync();
                }


                //Application.Current.Resources["ScreenWidth"] = ScreenSize.Width = Device.;
                Application.Current.Resources["WindowsWidth"] = WindowsSize.Width = Window.Current.Bounds.Width;
                //Screen.Width = Window.Current.Bounds.Width;
                Window.Current.SizeChanged += Current_SizeChanged;
                //Window.Current.Bounds.Heigh
                //ApplicationView.GetForCurrentView().VisibleBounds.Width;

                NetworkStatusChange();

                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();
                IStateCondition condition = new StateCondition(1200);
                condition.Configurate(rootFrame, Window.Current.Bounds.Width);
                Manager.DeviceService = new DeviceService(Manager, condition);
                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                if (e.PreviousExecutionState == ApplicationExecutionState.Suspended)
                {

                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                NavigationService = new NavigationService();
                ConfigPage(NavigationService);
                NavigationService.Configure(FrameKey.RootFrame, rootFrame);

                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                Manager.KeyboardService.Init();

                NavigationService.NavigateTo(Pages.Container, e.Arguments, FrameKey.RootFrame);

                var last_log = Manager.SettingsService.GetValueLocal<string>("last-log");
                if (!string.IsNullOrEmpty(last_log))
                {
                    Manager.LogService.Log(last_log);
                    await Manager.LogService.WriteLog();

                    Dictionary<string, string> attribute = new Dictionary<string, string>();
                    attribute.Add("message", last_log);
                    Manager.TrackingService.TagEvent(LocalyticsEvent.Crash, attribute);
                    Manager.LogService.LogText = "";
                    Manager.SettingsService.RemoveValueLocal("last-log");
                }
            }
            // Ensure the current window is active
            Window.Current.Activate();
        }

        public void ConfigPage(NavigationService service)
        {
            service.ConfigurePage(Pages.Container, typeof(ContainerMaster));
            service.ConfigurePage(Pages.HomePage, typeof(Home_Page));
            service.ConfigurePage(Pages.DetailPage, typeof(Detail_Page));
            service.ConfigurePage(Pages.List_Categories_Page, typeof(List_Categories_Page));
            service.ConfigurePage(Pages.List_Articles_Page, typeof(List_Articles_Page));
            service.ConfigurePage(Pages.Saved_Articles_Page, typeof(Saved_Articles_Page));
            service.ConfigurePage(Pages.Currency, typeof(Currency_Page));
            service.ConfigurePage(Pages.Flash, typeof(Flash_Page));
            service.ConfigurePage(Pages.Gold, typeof(Gold_Page));
            service.ConfigurePage(Pages.MarkDown, typeof(MarkDown_Page));
            service.ConfigurePage(Pages.AddPaper, typeof(AddPaper_Page));

            //service.ConfigurePage(Pages.Weather, typeof(Weather_Page));
            //service.ConfigurePage(Pages.OCR, typeof(OCR_Page));
        }

        private void Current_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            Application.Current.Resources["WindowsWidth"] = WindowsSize.Width = Window.Current.Bounds.Width;
            //Screen.Width = Window.Current.Bounds.Width;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        protected override void OnShareTargetActivated(ShareTargetActivatedEventArgs e)
        {
            base.OnShareTargetActivated(e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>


        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            Manager.CameraService?.Dispose(true);
            deferral.Complete();
        }

        #region Check Network

        public static bool CheckInternetConnectivity()
        {
            var internetProfile = NetworkInformation.GetInternetConnectionProfile();
            if (internetProfile == null)
                return false;

            return (internetProfile.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess);
        }

        bool registeredNetworkStatusNotif = false;
        void NetworkStatusChange()
        {
            // register for network status change notifications
            try
            {
                networkStatusCallback = new NetworkStatusChangedEventHandler(OnNetworkStatusChange);
                if (!registeredNetworkStatusNotif)
                {
                    NetworkInformation.NetworkStatusChanged += networkStatusCallback;
                    registeredNetworkStatusNotif = true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        void OnNetworkStatusChange(object sender)
        {
            try
            {
                // get the ConnectionProfile that is currently used to connect to the Internet                
                ConnectionProfile InternetConnectionProfile = NetworkInformation.GetInternetConnectionProfile();

                if (InternetConnectionProfile == null)
                {
                    Debug.WriteLine("network not available");
                    //App.SportacamDataContext.InternetAvailable = false;
                }
                else
                {
                    if (InternetConnectionProfile.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess)
                        //App.SportacamDataContext.InternetAvailable = true;
                        Debug.WriteLine("network available");
                    //else
                    //    App.SportacamDataContext.InternetAvailable = false;
                    Debug.WriteLine("WIFI: " + InternetConnectionProfile.IsWlanConnectionProfile);
                    Debug.WriteLine("CELLULAR: " + InternetConnectionProfile.IsWwanConnectionProfile);

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        #endregion
    }
}
