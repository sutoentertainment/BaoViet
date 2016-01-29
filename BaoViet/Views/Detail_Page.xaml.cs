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
using Windows.Graphics.Display;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace BaoViet.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Detail_Page : BindablePage
    {

        public Detail_ViewModel ViewModel
        {
            get
            {
                return this.DataContext as Detail_ViewModel;
            }
        }
        //public WebView webView = new WebView(WebViewExecutionMode.SeparateThread);

        public Detail_Page()
        {
            this.InitializeComponent();
            App.Current.OnRefreshRequested += App_OnRefreshRequested;
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

            webView.NavigationCompleted += webView_NavigationCompleted;
            webView.NavigationFailed += webView_NavigationFailed;
            webView.DOMContentLoaded += webView_DOMContentLoaded;
            webView.NavigationStarting += webView_NavigationStarting;
            webView.NavigationFailed += WebView_NavigationFailed;
            
            webView.ScriptNotify += WebView_ScriptNotify;
            ViewModel.WebViewControl = webView;
            //webViewContainer.Children.Add(webView);

            await ViewModel.LoadPageUsingReadability();
            base.OnNavigatedTo(e);
        }

        float pointerY;
        private async void WebView_ScriptNotify(object sender, NotifyEventArgs e)
        {
            try
            {
                string SY = (string)await webView.InvokeScriptAsync("getSY", new List<string>());
                pointerY = float.Parse(SY);
                //MenuContextGrid.RenderTransformOrigin = new Point(0.5, 0.5);
                //transform.Y = pointerY * 100 / App.Current.Host.Content.ScaleFactor;
                //MenuContextGrid.RenderTransform = transform;
            }
            catch { }

            ViewModel.ImageLocation = e.Value;
            
            MenuContainerTransform.Y = pointerY;

            FlyoutBase flyoutBase = FlyoutBase.GetAttachedFlyout(MenuContainer);
            flyoutBase.ShowAt(MenuContainer);
        }

        private void WebView_NavigationFailed(object sender, WebViewNavigationFailedEventArgs e)
        {
            App.Current.InvokeOnToastRise("Lỗi khi tải trang, hãy thử refresh lại trang web bằng nút F5", 4000);
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
            if (webView.DocumentTitle != "")
                ViewModel.CurrentWebTitle = webView.DocumentTitle;
            if (e.Uri != null)
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

            webViewDetail = null;
            webViewDetailContainer.Children.Clear();
            webViewContainer.Children.Clear();
            App.Current.OnRefreshRequested -= App_OnRefreshRequested;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            //GC.Collect();
            base.OnNavigatedFrom(e);
        }
    }
}
