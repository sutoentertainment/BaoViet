using BaoViet.Views;
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

// The Blank Application template is documented at http://go.microsoft.com/fwlink/?LinkId=402347&clcid=0x409

namespace BaoViet
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application, IApp
    {

        public bool CustomBackPressed = false;
        /// <summary>
        /// Allows tracking page views, exceptions and other telemetry through the Microsoft Application Insights service.
        /// </summary>
        //public static Microsoft.ApplicationInsights.TelemetryClient TelemetryClient;

        /// <summary>
        /// React to network status changed
        /// </summary>
        public NetworkStatusChangedEventHandler networkStatusCallback { get; set; }

        new public static App Current { get; set; }
        public Manager Manager { get; set; }

        public Frame MasterFrame { get; set; }
        public RootDataContext RootDataContext { get; set; }

        public TileManager TileManager { get; set; }
        public NavigationService NavigationService { get; internal set; }

        public delegate void OnToastActivatedEventHandler(string text, double milisecs);

        public event OnToastActivatedEventHandler OnToastRise;

        public delegate void OnToastTappedEventHandler(ToastAction action);

        public event OnToastTappedEventHandler OnToastTapped;

        public delegate void OnProtocolActivatedEventHandler(string param);

        public event OnProtocolActivatedEventHandler OnProtocolActivated;

        public delegate void OnBackRequestedEventHandler();

        public event OnBackRequestedEventHandler OnBackRequested;

        public delegate void OnRefreshRequestedEventHandler();

        public event OnRefreshRequestedEventHandler OnRefreshRequested;

        public delegate void OnAppResumeEventHandler();

        public event OnAppResumeEventHandler OnAppResumed;

        public void InvokeOnBackRequested()
        {
            if (OnBackRequested != null)
                OnBackRequested.Invoke();
        }

        internal void InvokeOnRefreshRequested()
        {
            if (OnRefreshRequested != null)
                OnRefreshRequested.Invoke();
        }


        public void InvokeOnToastRise(string text, double milisec)
        {
            if (OnToastRise != null)
                OnToastRise.Invoke(text, milisec);
        }

        public void InvokeOnToastTapped(ToastAction action)
        {
            if (OnToastTapped != null)
                OnToastTapped.Invoke(action);
        }

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            //TelemetryClient = new Microsoft.ApplicationInsights.TelemetryClient();

            this.InitializeComponent();
            this.Suspending += OnSuspending;
            this.Resuming += App_Resuming;
            this.UnhandledException += App_UnhandledException;
            Current = this;
            Manager = new Manager();
            Manager.Database.CreateTable<FeedItem>();
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

        private async void App_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            //TelemetryClient.TrackException(e.Exception);
            Manager.LogService.Log(e.ToString());
            Manager.LogService.Log(e.Message);
            Manager.LogService.Log(e.Exception.ToString());
            Manager.LogService.Log(e.Exception.StackTrace);
            Manager.LogService.Log(e.Exception.Message);
            await Manager.LogService.WriteLog();
        }

        public void OnToastActivated_Invoke(string text, double milisecs)
        {
            if (OnToastRise != null)
                OnToastRise.Invoke(text, milisecs);
        }

        public void App_Resuming(object sender, object e)
        {
            if (OnAppResumed != null)
                OnAppResumed.Invoke();
        }


        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override async void OnLaunched(LaunchActivatedEventArgs e)
        {
            Manager.TrackingService.AutoIntegrate();
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif
            
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

            Frame rootFrame = Window.Current.Content as Frame;
            // Register to network changed event
            NetworkStatusChange();
            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }
            NavigationService = new NavigationService();
            NavigationService.ConfigPage();
            NavigationService.Configure(ViewModels.FrameKey.RootFrame, rootFrame);


            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                Manager.Configure();

                NavigationService.NavigateTo(Pages.Container, e.Arguments, ViewModels.FrameKey.RootFrame);
            }
            // Ensure the current window is active
            Window.Current.Activate();
            await Manager.RateUsService.ShowRatePopup(5, true, "đánh giá 5 sao", "để lần sau", "Gửi đánh giá", "Xin hãy dành chút thời gian ủng hộ phần mềm, đây là động lục giúp nhóm pháp triển phầm mềm để phục vụ bạn tốt hơn.");
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
        protected override void OnActivated(IActivatedEventArgs e)
        {
            if(e.Kind == ActivationKind.Protocol)
            {
                var protocolArgs = e as ProtocolActivatedEventArgs;
                var uri = protocolArgs.Uri;

                if (OnProtocolActivated != null)
                    OnProtocolActivated.Invoke(uri.ToString());
            }
            base.OnActivated(e);
        }

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
