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
using BaoViet.Helpers;
using Newtonsoft.Json;
using BaoViet.Models;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace BaoViet.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Detail_Page : Page
    {

        public Detail_Page_ViewModel ViewModel
        {
            get
            {
                return this.DataContext as Detail_Page_ViewModel;
            }
        }
        //public WebView webView = new WebView(WebViewExecutionMode.SeparateThread);

        public Detail_Page()
        {
            this.InitializeComponent();
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

            ViewModel.IsBusy = true;

            var address = "";
            address = ViewModel.CurrentFeed.Link;
            address = address.ToReadability();

            var response_content = await App.WebService.GetString(address);
            var response = JsonConvert.DeserializeObject<ReadabilityResponse>(response_content);

            webView.NavigationCompleted += webView_NavigationCompleted;
            webView.NavigationFailed += webView_NavigationFailed;
            webView.DOMContentLoaded += webView_DOMContentLoaded;
            webView.NavigationStarting += webView_NavigationStarting;
            webView.NavigationFailed += WebView_NavigationFailed;
            ViewModel.WebViewControl = webView;
            //webViewContainer.Children.Add(webView);

            await Task.Delay(200);
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                //webView.Navigate(new Uri(address));
                webView.NavigateToString(response.content);
                ViewModel.CurrentWebPage = new Uri(address);
            });
            base.OnNavigatedTo(e);
        }

        private void WebView_NavigationFailed(object sender, WebViewNavigationFailedEventArgs e)
        {
            App.InvokeOnToastRise("Lỗi khi tải trang, hãy thử refresh lại trang web bằng nút F5", 4000);
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
            //UNDONE
            //App.InvokeOnToastRise("Lỗi khi tải trang, hãy thử refresh lại trang web bằng nút F5", 4000);
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
            //GC.Collect();
            //GC.Collect();
            base.OnNavigatedFrom(e);
        }
    }
}
