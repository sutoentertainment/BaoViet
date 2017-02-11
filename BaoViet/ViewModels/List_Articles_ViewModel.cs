using BaoViet.Interfaces;
using BaoVietCore.Factory;
using BaoVietCore.Interfaces;
using BaoVietCore.Models;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;
using WinUX;

namespace BaoViet.ViewModels
{
    public class List_Articles_ViewModel : ViewModelBase, INavigable, ITrackingAble
    {
        Category _CurrentCategory;
        public Category CurrentCategory
        {
            get
            {
                return _CurrentCategory;
            }
            set
            {
                Set(ref _CurrentCategory, value);
            }

        }
        public ObservableCollection<IFeedItem> ListFeed { get; set; }

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

        public List_Articles_ViewModel()
        {
            ListFeed = new ObservableCollection<IFeedItem>();
        }

        public async void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Back)
                return;

            await LoadFeed();
            App.Current.OnRefreshRequested += Current_OnRefreshRequested;
        }

        private async Task LoadFeed()
        {
            ListFeed.Clear();
            await Task.Delay(100);
            IsBusy = true;
            try
            {
                //var result = await CurrentCategory.Owner.GetFeed(CurrentCategory.Source);
                var XmlParser = XmlParserFactory.Create(CurrentCategory.Owner.Type);
                var result = await App.Current.Manager.RssService.GetFeed(XmlParser, CurrentCategory.Source);
                //if (result.Paper == CurrentCategory.Owner.Type)
                ListFeed.AddRange(result);
            }
            catch { }
            IsBusy = false;
        }

        private async void Current_OnRefreshRequested()
        {
            await LoadFeed();
        }

        public void OnNavigatedFrom(NavigationEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Back)
            {
                App.Current.Manager.WebService.CancelCurrentRequests();
            }
            App.Current.OnRefreshRequested -= Current_OnRefreshRequested;

        }

        public void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {

        }

        public bool AllowGoBack()
        {
            return true;
        }

        public string ScreenName
        {
            get
            {
                return Localytics.LocalyticsScreen.ListArticlePage;
            }
        }
    }
}
