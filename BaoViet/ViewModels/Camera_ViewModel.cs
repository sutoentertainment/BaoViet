using GalaSoft.MvvmLight;
using BaoViet.Interfaces;
using System;
using Windows.UI.Xaml.Navigation;

namespace BaoViet.ViewModels
{
    public class Camera_ViewModel : ViewModelBase, INavigable
    {
        public bool AllowGoBack()
        {
            return true;
        }

        public async void OnNavigatedFrom(NavigationEventArgs e)
        {
            
            await App.Current.Manager.CameraService.Dispose(true);
        }

        public void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        public void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            
        }
    }
}