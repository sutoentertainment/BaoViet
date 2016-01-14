using BaoViet.Interfaces;
using BaoViet.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Croft.Core.Extensions;
using BaoViet.Models;
using Microsoft.Practices.ServiceLocation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace BaoViet.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class List_Articles_Page : Page
    {
        public List_Articles_ViewModel ViewModel
        {
            get
            {
                return DataContext as List_Articles_ViewModel;
            }
        }
        public List_Articles_Page()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.NavigationMode == NavigationMode.Back)
                return;

            ViewModel.ListFeed.Clear();
            ViewModel.IsBusy = true;
            var feeds = await ViewModel.CurrentCategory.Owner.GetFeed(ViewModel.CurrentCategory.Source);
            ViewModel.ListFeed.AddRange(feeds);
            ViewModel.IsBusy = false;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            if (e.NavigationMode == NavigationMode.Back)
                App.WebService.CancelCurrentRequests();
        }

        private void ListArticle_ItemClick(object sender, ItemClickEventArgs e)
        {
            var feed = e.ClickedItem as FeedItem;
            var detail = ServiceLocator.Current.GetInstance<Detail_Page_ViewModel>();
            detail.CurrentFeed = feed;
            App.MasterFrame.Navigate(typeof(Detail_Page));
        }

        private void SlidableListItem_RightCommandRequested(object sender, EventArgs e)
        {
            App.InvokeOnToastRise("Đã lưu", 1000);
        }
    }
}
