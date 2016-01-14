using BaoViet.Models;
using BaoViet.ViewModels;
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
    public sealed partial class List_Categories_Page : Page
    {
        public List_Categories_ViewModel ViewModel
        {
            get
            {
                return this.DataContext as List_Categories_ViewModel;
            }
        }

        public List_Categories_Page()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.NavigationMode == NavigationMode.Back)
                return;
            ViewModel.HeaderLoaded = true;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            if(e.NavigationMode == NavigationMode.Back)
            {
                ViewModel.HeaderLoaded = false;
            }
        }

        private void Category_ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var cate = e.ClickedItem as Category;
            var vm = ServiceLocator.Current.GetInstance<List_Articles_ViewModel>();
            vm.CurrentCategory = cate;
            App.MasterFrame.Navigate(typeof(List_Articles_Page));
        }
    }
}
