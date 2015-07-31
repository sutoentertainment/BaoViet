using BaoViet.Models;
using BaoViet.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace BaoViet.DataContext
{
    public class RootDataContext : BaseModel
    {
        public RelayCommand<NavigationEventArgs> OnNavigatedCommand { get; set; }
        Action<NavigationEventArgs> OnNavigatedAction;

        public RootDataContext()
        {
            OnNavigatedAction = new Action<NavigationEventArgs>(OnNavigated);
            OnNavigatedCommand = new RelayCommand<NavigationEventArgs>(OnNavigatedAction);
        }

        public void OnNavigated(NavigationEventArgs e)
        {
            //TODO: Display back button
            // Register a handler for BackRequested events and set the
            // visibility of the Back button
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                App.MasterFrame.CanGoBack ?
                AppViewBackButtonVisibility.Visible :
                AppViewBackButtonVisibility.Collapsed;

        }
    }
}
