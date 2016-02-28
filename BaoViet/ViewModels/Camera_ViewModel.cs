using GalaSoft.MvvmLight;
using BaoViet.Interfaces;
using System;
using Windows.UI.Xaml.Navigation;

namespace BaoViet.ViewModels
{
    public class Camera_ViewModel : ViewModelBase, INavigable
    {
        bool _LightOn = false;
        public bool LightOn
        {
            get
            {
                return _LightOn;
            }
            set
            {
                Set(ref _LightOn, value);
            }
        }
        public bool AllowGoBack()
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationEventArgs e)
        {

            App.Current.Manager.CameraService.Dispose(true);
        }

        public void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        public void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {

        }
    }
}