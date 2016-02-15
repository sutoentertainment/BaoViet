using BaoViet.Interfaces;
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
using WinUX.Extensions;

namespace BaoViet.ViewModels
{
    public class Saved_Articles_ViewModel : ViewModelBase, INavigable, ITrackingAble
    {
        public ObservableCollection<IFeedItem> ListFeed { get; set; }

        public string ScreenName
        {
            get
            {
                return Localytics.LocalyticsScreen.SavedArticlePage;
            }
        }

        public Saved_Articles_ViewModel()
        {
            ListFeed = new ObservableCollection<IFeedItem>();
        }

        public void LoadData()
        {
            ListFeed.Clear();
            ListFeed.AddRange(App.Current.Manager.Database.GetItems<FeedItem>());
        }

        public void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Back)
                return;
            LoadData();
        }

        public void OnNavigatedFrom(NavigationEventArgs e)
        {
        }

        public void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {

        }

        public bool AllowGoBack()
        {
            return true;
        }
    }
}
