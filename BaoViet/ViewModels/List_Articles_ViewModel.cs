﻿using BaoViet.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoViet.ViewModels
{
    public class List_Articles_ViewModel : BaseModel
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
                _CurrentCategory = value;
                RaisePropertyChanged("CurrentCategory");
            }

        }
        public ObservableCollection<FeedItem> ListFeed { get; set; }

        public List_Articles_ViewModel()
        {
            ListFeed = new ObservableCollection<FeedItem>();
        }
    }
}
