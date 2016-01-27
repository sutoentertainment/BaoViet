using BaoViet.Interfaces;
using BaoVietCore.Interfaces;
using BaoVietCore.Models;
using Croft.Core.Extensions;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;

namespace BaoViet.ViewModels
{
    public class Saved_Articles_ViewModel : ViewModelBase, INavigable
    {
        public ObservableCollection<IFeedItem> ListFeed { get; set; }

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
