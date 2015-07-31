using BaoViet.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace BaoViet.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Detail_Page : Page
    {

        public Detail_Page_ViewModel ViewModel { get; set; }
        //public WebView webView = new WebView(WebViewExecutionMode.SeparateThread);

        public Detail_Page()
        {
            this.InitializeComponent();
            ViewModel = new Detail_Page_ViewModel();
            this.DataContext = ViewModel;
            App.OnRefreshRequested += App_OnRefreshRequested;
        }

        ~Detail_Page()
        {

        }

        private void App_OnRefreshRequested()
        {
            ViewModel.RefreshCommand.Execute(null);
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Back)
                return;

            var address = "";
            //TODO: handle backpress event for phone
            if(e.Parameter != null)
            {
                address = e.Parameter.ToString();
            }

            webView.NavigationCompleted += webView_NavigationCompleted;
            webView.NavigationFailed += webView_NavigationFailed;
            webView.DOMContentLoaded += webView_DOMContentLoaded;
            webView.NavigationStarting += webView_NavigationStarting;
            ViewModel.WebViewControl = webView;
            //webViewContainer.Children.Add(webView);

            await Task.Delay(500);
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                webView.Navigate(new Uri(address));
                ViewModel.CurrentWebPage = new Uri(address);
            });
            base.OnNavigatedTo(e);
        }

        private void webView_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs e)
        {
            ViewModel.IsBusy = false;
        }

        private void webView_NavigationFailed(object sender, WebViewNavigationFailedEventArgs e)
        {
            ViewModel.IsBusy = false;
        }

        private void webView_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs e)
        {
            ViewModel.IsBusy = true;
        }

        private void webView_DOMContentLoaded(WebView sender, WebViewDOMContentLoadedEventArgs e)
        {
            ViewModel.CurrentWebTitle = webView.DocumentTitle;
            ViewModel.CurrentWebPage = e.Uri;
            ViewModel.IsBusy = false;
            ViewModel.UpdateButton();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            webView.NavigationCompleted -= webView_NavigationCompleted;
            webView.NavigationFailed -= webView_NavigationFailed;
            webView.DOMContentLoaded -= webView_DOMContentLoaded;
            webView.NavigationStarting -= webView_NavigationStarting;
            webView = null;
            ViewModel.WebViewControl = null;
            webViewContainer.Children.Clear();
            App.OnRefreshRequested -= App_OnRefreshRequested;
            GC.Collect();
            //GC.Collect();
            base.OnNavigatedFrom(e);
        }
    }
}
