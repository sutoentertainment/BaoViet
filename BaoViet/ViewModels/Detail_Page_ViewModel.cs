using BaoViet.Models;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;

namespace BaoViet.ViewModels
{
    public class Detail_Page_ViewModel : BaseModel
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
                _CurrentWebPage = value;
                RaisePropertyChanged("CurrentWebPage");
            }
        }

        public bool CanGoBack
        {
            get
            {
                return WebViewControl.CanGoBack;
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

        public RelayCommand ForwardCommand { get; set; }

        public RelayCommand RefreshCommand { get; set; }

        bool _IsBusy = false;
        public bool IsBusy
        {
            get
            {
                return _IsBusy;
            }
            set
            {
                _IsBusy = value;
                RaisePropertyChanged("IsBusy");
            }
        }


        public Detail_Page_ViewModel()
        {
            CurrentWebTitle = "";

            IsBackEnable = IsForwardEnable = false;
            
            ShareCommand = new RelayCommand(Share);
            
            BackCommand = new RelayCommand(Back);
            
            ForwardCommand = new RelayCommand(Forward);
            
            RefreshCommand = new RelayCommand(Refresh);
        }

        #region ShareLink
        DataTransferManager dataTransferManager;

        public void Share()
        {
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
            request.Data.SetWebLink(CurrentWebPage);
        }

        #endregion

        void Back()
        {
            if (WebViewControl.CanGoBack)
            {
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

        void Refresh()
        {
            WebViewControl.Refresh();
        }
    }
}
