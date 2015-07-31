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
using BaoViet.Helpers;
using BaoViet.ViewModels;
using Windows.UI.Input;
using System.Diagnostics;
using BaoViet.Models;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace BaoViet.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Home_Page : Page
    {
        
        public Home_Page_ViewModel ViewModel { get; set; }
        GestureRecognizer gestureRecognizer = new Windows.UI.Input.GestureRecognizer();

        public Home_Page()
        {
            this.InitializeComponent();
            ViewModel = new Home_Page_ViewModel();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
        }

        private void HamburgerButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Debug.WriteLine("hit");
            ViewModel.IsPaneOpen = !ViewModel.IsPaneOpen;
        }

        private void LayoutRoot_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            //Debug.WriteLine(e.Delta.Translation.X);
            //Debug.WriteLine(e.Cumulative.Translation.X);
            if(e.Delta.Translation.X >= 4 && e.Cumulative.Translation.X >= 250)
            {
                ViewModel.IsPaneOpen = true;
            }
            else if (e.Delta.Translation.X <= -4 && e.Cumulative.Translation.X <= -250)
            {
                ViewModel.IsPaneOpen = false;

            }
        }

        private void Grid_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            if(e.Cumulative.Translation.X >= 100)
            {
                ViewModel.IsPaneOpen = true;
            }
        }

        private void FrontPage_ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var paper = e.ClickedItem as Paper;
            App.MasterFrame.Navigate(typeof(Detail_Page), paper.HomePage);

            //TODO: Prepare the data model for next page

            //this.Frame.Navigate(typeof(List_Articles_Page));
        }

        /// <summary>
        /// Change tile
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            TileManager tile = new TileManager();
            tile.UpdateTile(TransparentTile.IsOn);
        }



        //ScrollViewer FrontPage_ScrollViewer;
        //private void FrontPage_ListView_Loaded(object sender, RoutedEventArgs e)
        //{
        //    FrontPage_ScrollViewer = FrontPage_ListView.GetFirstDescendantOfType<ScrollViewer>();
        //    //FrontPage_ScrollViewer.ViewChanged += FrontPage_ScrollViewer_ViewChanged;

        //    //New API

        //    FrontPage_ScrollViewer.RegisterPropertyChangedCallback(ScrollViewer.VerticalOffsetProperty, FrontPage_OffsetChanged);
        //}

        //private void FrontPage_OffsetChanged(DependencyObject sender, DependencyProperty dp)
        //{
        //    var offset = (double)sender.GetValue(dp);

        //    tracking.Value = offset / FrontPage_ScrollViewer.ScrollableHeight * 100;
        //}

        //private void FrontPage_ScrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        //{
        //    tracking.Value = FrontPage_ScrollViewer.VerticalOffset / FrontPage_ScrollViewer.ScrollableHeight * 100;
        //}
    }
}
