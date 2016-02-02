using BaoViet.Interfaces;
using BaoViet.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace BaoViet.Views
{
    public class BindablePage : Page
    {
        public static BindablePage CurrentPage { get; set; }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            CurrentPage = this;
            base.OnNavigatedTo(e);

            var navigableViewModel = this.DataContext as INavigable;
            if (navigableViewModel != null)
            {
                if (navigableViewModel is ITrackingAble)
                {
                    App.Current.Manager.TrackingService.TagScreen((navigableViewModel as ITrackingAble).ScreenName);
                }
                navigableViewModel.OnNavigatedTo(e);
                SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;
            }
        }

        public void OnBackRequested(object sender, BackRequestedEventArgs e)
        {
            var navigableViewModel = this.DataContext as INavigable;
            if (navigableViewModel != null)
            {
                if (navigableViewModel.AllowGoBack())
                {
                    if (App.Current.NavigationService.CanGoBack)
                    {
                        App.Current.NavigationService.GoBack();
                        //Prevent out app
                        if (e != null)
                            e.Handled = true;
                    }
                }
                else
                {
                    if (e != null)
                        //Prevent out app
                        e.Handled = true;
                }

            }
            else
            {
                if (App.Current.NavigationService.CanGoBack)
                {
                    App.Current.NavigationService.GoBack();
                    //Prevent out app
                    if (e != null)
                        e.Handled = true;
                }
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            var navigableViewModel = this.DataContext as INavigable;
            if (navigableViewModel != null)
            {
                navigableViewModel.OnNavigatedFrom(e);
                SystemNavigationManager.GetForCurrentView().BackRequested -= OnBackRequested;
            }
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);

            var navigableViewModel = this.DataContext as INavigable;
            if (navigableViewModel != null)
                navigableViewModel.OnNavigatingFrom(e);
        }

    }
}
