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
using ThHelper;
using Windows.ApplicationModel.Store;
using Microsoft.Practices.ServiceLocation;
using BaoViet.Interfaces;
using BaoVietCore.Interfaces;
using BaoVietCore.Models;
using BaoVietCore.Helpers;
using Windows.UI.Xaml.Media.Animation;
using BaoViet.Models;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace BaoViet.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Home_Page : BindablePage
    {

        public Home_Page_ViewModel ViewModel
        {
            get
            {
                return this.DataContext as Home_Page_ViewModel;
            }
        }
        GestureRecognizer gestureRecognizer = new Windows.UI.Input.GestureRecognizer();

        public Home_Page()
        {
            this.InitializeComponent();

            this.Loaded += Home_Page_Loaded;
            this.SizeChanged += Home_Page_SizeChanged;
        }

        double sideMenuMaxY
        {
            get
            {
                return this.Frame.RenderSize.Height - 40 - 40 - 132;
            }
        }

        private void Home_Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (!IsSideMenuOpen)
                sideMenuTransform.Y = sideMenuMaxY;

            if (e.NewSize.Width > 720)
            {
                App.Current.NavigationService.Configure(FrameKey.PaneSplitFrame, PaneFrame);
            }
        }

        private void Home_Page_Loaded(object sender, RoutedEventArgs e)
        {
            sideMenuTransform.Y = sideMenuMaxY;
            this.Loaded -= Home_Page_Loaded;
        }

        bool IsSideMenuOpen = false;

        private void ToggleSideMenu(object sender, TappedRoutedEventArgs e)
        {
            IsSideMenuOpen = !IsSideMenuOpen;
            AnimateSideMenu(IsSideMenuOpen);
        }

        private void AnimateSideMenu(bool isSideMenuOpen)
        {
            if (isSideMenuOpen)
            {
                DoubleAnimation animation = new DoubleAnimation();
                animation.To = 0;
                animation.Duration = TimeSpan.FromMilliseconds(300);
                QuadraticEase ease = new QuadraticEase();
                ease.EasingMode = EasingMode.EaseIn;
                animation.EasingFunction = ease;

                Storyboard storyboard = new Storyboard();
                Storyboard.SetTarget(animation, sideMenuTransform);
                Storyboard.SetTargetProperty(animation, "Y");
                storyboard.Children.Add(animation);
                storyboard.Begin();


            }
            else
            {
                DoubleAnimation animation = new DoubleAnimation();
                animation.To = sideMenuMaxY;
                animation.Duration = TimeSpan.FromMilliseconds(400);
                BounceEase ease = new BounceEase();
                ease.Bounces = 4;
                ease.Bounciness = 4;
                ease.EasingMode = EasingMode.EaseOut;
                animation.EasingFunction = ease;

                Storyboard storyboard = new Storyboard();
                Storyboard.SetTarget(animation, sideMenuTransform);
                Storyboard.SetTargetProperty(animation, "Y");
                storyboard.Children.Add(animation);
                storyboard.Begin();
            }
        }

        private void LayoutRoot_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            //Debug.WriteLine(e.Delta.Translation.X);
            //Debug.WriteLine(e.Cumulative.Translation.X);
            if (e.Delta.Translation.X >= 4 && e.Cumulative.Translation.X >= 250)
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
            if (e.Cumulative.Translation.X >= 100)
            {
                ViewModel.IsPaneOpen = true;
            }
        }

        /// <summary>
        /// Change tile
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            await App.Current.TileManager.CreateSecondaryTileAsync("TransparentTile", "ms-appx:///Assets/Square150x150LogoTrans.png", "Báo Việt", "?page=main", "ms-appx:///Assets/Wide310x150LogoTrans.png");
        }

        private void SideMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.IsPaneOpen = false;
            if (SideMenu.SelectedIndex == 0)
                rootPivot.SelectedIndex = 0;
            if (SideMenu.SelectedIndex == 1)
                App.Current.NavigationService.NavigateTo(Pages.Saved_Articles_Page);
            if (SideMenu.SelectedIndex == 2)
                rootPivot.SelectedIndex = 1;
        }

        private void SlideMenu_ItemClick(object sender, ItemClickEventArgs e)
        {
            ViewModel.IsPaneOpen = false;
            var menuItem = e.ClickedItem as MenuItemBase;
            switch (menuItem.Type)
            {
                case MenuItemType.Home:
                    rootPivot.SelectedIndex = 0;
                    break;
                case MenuItemType.Saved:
                    App.Current.NavigationService.NavigateTo(Pages.Saved_Articles_Page);
                    break;
                case MenuItemType.Setting:
                    rootPivot.SelectedIndex = 1;
                    break;
            }
        }

        private async void RateUs_Tapped(object sender, TappedRoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-windows-store:reviewapp?appid=" + CurrentApp.AppId));
        }

        private async void Suggest_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var mailto = new Uri("mailto:?to=thang2410199@gmail.com&subject=Góp ý cho Báo Việt&body=");
            await Windows.System.Launcher.LaunchUriAsync(mailto);
        }

        private void RemoveAd_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (!App.Current.Manager.IAPService.CheckProduct("Remove_Ads"))
                App.Current.Manager.IAPService.BuyProduct("Donate");
        }

        private void Donate_Tapped(object sender, TappedRoutedEventArgs e)
        {
            App.Current.Manager.IAPService.BuyProduct("Donate");
        }

        private void SideMenuListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            ViewModel.IsPaneOpen = false;
            var context = e.ClickedItem as IMenuItem;
            context.OnClicked();
        }

        private void SideMenu_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            if (sideMenuTransform.Y >= sideMenuMaxY - 100)
            {
                IsSideMenuOpen = false;
            }
            else
            {
                IsSideMenuOpen = true;
            }
            AnimateSideMenu(IsSideMenuOpen);
        }

        private void SideMenu_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (sideMenuTransform.Y >= 0 && sideMenuTransform.Y <= sideMenuMaxY && IsSideMenuOpen == false)
                sideMenuTransform.Y += e.Delta.Translation.Y;

        }

        private void SideMenu_ManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {

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
