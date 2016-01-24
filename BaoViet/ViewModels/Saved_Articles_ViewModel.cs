using BaoVietCore.Models;
using Croft.Core.Extensions;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoViet.ViewModels
{
    public class Saved_Articles_ViewModel : ViewModelBase
    {
        public ObservableCollection<FeedItem> ListFeed { get; set; }

        public Saved_Articles_ViewModel()
        {
            ListFeed = new ObservableCollection<FeedItem>();
        }
        
        public void LoadData()
        {
            ListFeed.Clear();
            ListFeed.AddRange(App.Current.Manager.Database.GetItems<FeedItem>());
        }
    }
}
