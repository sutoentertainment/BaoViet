using BaoViet.ViewModels;
using BaoVietCore.Interfaces;
using BaoVietCore.Models;
using Microsoft.Practices.ServiceLocation;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace BaoViet.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Saved_Articles_Page : BindablePage
    {
        public Saved_Articles_ViewModel ViewModel
        {
            get
            {
                return this.DataContext as Saved_Articles_ViewModel;
            }
        }
        public Saved_Articles_Page()
        {
            this.InitializeComponent();
        }

        private void ListArticle_ItemClick(object sender, ItemClickEventArgs e)
        {
            var feed = e.ClickedItem as IFeedItem;
            var detail = ServiceLocator.Current.GetInstance<Detail_ViewModel>();
            detail.CurrentFeed = feed;
            App.Current.NavigationService.NavigateTo(Pages.DetailPage);
        }

        private void SlidableListItem_RightCommandRequested(object sender, EventArgs e)
        {
            var item = (sender as FrameworkElement).DataContext as FeedItem;
            ViewModel.ListFeed.Remove(item);
            App.Current.Manager.Database.Delete(item);
        }
    }
}
