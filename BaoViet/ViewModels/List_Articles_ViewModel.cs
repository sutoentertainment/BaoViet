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
    public class List_Articles_ViewModel : ViewModelBase, INavigable
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

            ListFeed.Clear();
            IsBusy = true;
            try
            {
                var result = await CurrentCategory.Owner.GetFeed(CurrentCategory.Source);
                if (result.Paper == CurrentCategory.Owner.Type)
                    ListFeed.AddRange(result.Feeds);
            }
            catch { }
            IsBusy = false;
        }

        public void OnNavigatedFrom(NavigationEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Back)
                App.Current.Manager.WebService.CancelCurrentRequests();
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
