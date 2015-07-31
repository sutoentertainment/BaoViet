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
        Action ShareAction;
        public WebView WebViewControl;

        public RelayCommand BackCommand { get; set; }
        Action BackAction;

        public RelayCommand ForwardCommand { get; set; }
        Action ForwardAction;

        public RelayCommand RefreshCommand { get; set; }
        Action RefreshAction;

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

            ShareAction = new Action(Share);
            ShareCommand = new RelayCommand(ShareAction);

            BackAction = new Action(Back);
            BackCommand = new RelayCommand(BackAction);

            ForwardAction = new Action(Forward);
            ForwardCommand = new RelayCommand(ForwardAction);

            RefreshAction = new Action(Refresh);
            RefreshCommand = new RelayCommand(RefreshAction);
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
