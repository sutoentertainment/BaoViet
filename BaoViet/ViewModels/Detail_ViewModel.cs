using BaoViet.Helpers;
using BaoViet.Interfaces;
using BaoVietCore.Helpers;
using BaoVietCore.Interfaces;
using BaoVietCore.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace BaoViet.ViewModels
{
    public class Detail_ViewModel : ViewModelBase, INavigable
    {
        Uri _CurrentWebPage = null;
        public Uri CurrentWebPage
        {
            get
            {
                return _CurrentWebPage;
            }
            set
            {
                Set(ref _CurrentWebPage, value);
            }
        }

        public bool CanGoBack
        {
            get
            {
                return WebViewControl.CanGoBack;
            }
        }

        bool _IsFullScreen = false;
        public bool IsFullScreen
        {
            get
            {
                return _IsFullScreen;
            }
            set
            {
                Set(ref _IsFullScreen, value);
            }
        }
        public bool CanGoForward
        {
            get
            {
                return WebViewControl.CanGoForward;
            }
        }

        public string CurrentWebTitle { get; set; }

        public bool IsBackEnable { get; set; }
        public bool IsForwardEnable { get; set; }

        public RelayCommand ShareCommand { get; set; }
        public WebView WebViewControl;

        public RelayCommand BackCommand { get; set; }
        public RelayCommand OpenWebCommand { get; set; }

        public RelayCommand ForwardCommand { get; set; }

        public RelayCommand RefreshCommand { get; set; }

        public RelayCommand DownloadImageCommand { get; set; }

        public RelayCommand OpenExternalCommand { get; set; }

        bool _IsBusy = false;
        public bool IsBusy
        {
            get
            {
                return _IsBusy;
            }
            set
            {
                Set(ref _IsBusy, value);
            }
        }
        IFeedItem _CurrentFeed;
        public IFeedItem CurrentFeed
        {
            get
            {
                return _CurrentFeed;
            }
            set
            {
                Set(ref _CurrentFeed, value);
            }
        }

        Uri _FullScreenSource;

        public Uri FullScreenSource
        {
            get
            {
                return _FullScreenSource;
            }
            set
            {
                Set(ref _FullScreenSource, value);
            }
        }

        public string ImageLocation { get; internal set; }

        public string ScreenName
        {
            get
            {
                return "Detail";
            }
        }

        public Detail_ViewModel()
        {
            CurrentWebTitle = "";

            IsBackEnable = IsForwardEnable = false;

            ShareCommand = new RelayCommand(Share);

            BackCommand = new RelayCommand(Back);

            ForwardCommand = new RelayCommand(Forward);

            RefreshCommand = new RelayCommand(Refresh);

            OpenWebCommand = new RelayCommand(OpenWeb);

            DownloadImageCommand = new RelayCommand(DownloadImage);

            OpenExternalCommand = new RelayCommand(OpenExternal);
        }

        private async void OpenExternal()
        {
            Uri uri;
            if (IsFullScreen)
                uri = FullScreenSource;
            else
                uri = new Uri(CurrentFeed.Link);
            //Open with edge
            await Windows.System.Launcher.LaunchUriAsync(uri);
        }

        private async void DownloadImage()
        {
            await App.Current.Manager.ImageService.SaveImageToStorage(ImageLocation);
        }

        private void OpenWeb()
        {
            IsFullScreen = true;
            FullScreenSource = new Uri(CurrentFeed.Link);
        }

        #region ShareLink
        DataTransferManager dataTransferManager;

        public void Share()
        {
            if (!IsFullScreen && CurrentWebPage == null)
                return;
            dataTransferManager = DataTransferManager.GetForCurrentView();
            dataTransferManager.DataRequested += new TypedEventHandler<DataTransferManager, DataRequestedEventArgs>(this.ShareLinkHandler);
            Windows.ApplicationModel.DataTransfer.DataTransferManager.ShowShareUI();
        }

        internal void UpdateButton()
        {
            RaisePropertyChanged("CanGoForward");
            RaisePropertyChanged("CanGoBack");
        }

        private void ShareLinkHandler(DataTransferManager sender, DataRequestedEventArgs e)
        {
            dataTransferManager.DataRequested -= new TypedEventHandler<DataTransferManager, DataRequestedEventArgs>(this.ShareLinkHandler);
            DataRequest request = e.Request;
            request.Data.Properties.Title = this.CurrentWebTitle;
            if (IsFullScreen)
                request.Data.SetWebLink(FullScreenSource);
            else
                request.Data.SetWebLink(CurrentWebPage);
        }

        #endregion

        void Back()
        {
            if (WebViewControl.CanGoBack)
            {
                IsBusy = true;
                WebViewControl.GoBack();
            }
        }

        void Forward()
        {
            if (WebViewControl.CanGoForward)
            {
                WebViewControl.GoForward();
            }
        }

        void Save()
        {
            //TODO: Save web uri and title to saved list, use SQL here
        }

        async void Refresh()
        {
            WebViewControl.Refresh();

            if (WebViewControl.Source == null)
            {
                await LoadPageUsingReadability();
            }
        }

        public async Task LoadPageUsingReadability()
        {
            IsBusy = true;

            var address = "";
            address = CurrentFeed.Link;
            CurrentWebTitle = CurrentFeed.Title;
            address = address.ToReadability();

            var response_content = await App.Current.Manager.WebService.GetString(address);
            var response = JsonConvert.DeserializeObject<ReadabilityResponse>(response_content);

            //response.content += @"<style>img{width:100% !important;}</style>";

            response.content += "<script>\r\n    function eventListener(evt) {\r\n        if (evt.detail == 1) {\r\n evt.preventDefault(); screenY = evt.clientY;\r\n            window.external.notify(evt.target.src); return false;\r\n        }\r\n    }\r\n\r\n    var gestureHandler = new Array();\r\n    var screenY;\r\n    var links = document.getElementsByTagName('img');\r\n    for (i = 0; i < links.length; i++) {\r\n        links[i].addEventListener('click', eventListener, false);\r\n    };\r\n function getSY(){	return screenY.toString();} </script>";


            
            await Task.Delay(200);

            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                if (response.content != null)
                    //webView.Navigate(new Uri(address));
                    WebViewControl.NavigateToString(response.content);
                else
                    WebViewControl.Navigate(new Uri(CurrentFeed.Link));
            });
            CurrentWebPage = new Uri(CurrentFeed.Link);
        }

        public bool AllowGoBack()
        {
            if (IsFullScreen)
            {
                IsFullScreen = false;
                return false;
            }
            return true;
        }

        public void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        public void OnNavigatedFrom(NavigationEventArgs e)
        {
        }

        public void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
        }
    }
}
