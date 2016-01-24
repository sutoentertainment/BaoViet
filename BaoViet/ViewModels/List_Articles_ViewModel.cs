using BaoVietCore.Models;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoViet.ViewModels
{
    public class List_Articles_ViewModel : ViewModelBase
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
        public ObservableCollection<FeedItem> ListFeed { get; set; }

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
            ListFeed = new ObservableCollection<FeedItem>();
        }
    }
}
