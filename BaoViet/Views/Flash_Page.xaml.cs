using BaoVietCore.Services;
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
    public sealed partial class Flash_Page : BindablePage
    {
        public Flash_Page()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            App.Current.Manager.CameraService = new BasicCameraService(App.Current.Manager);
            await App.Current.Manager.CameraService.PrepareInitAsync();
            if (App.Current.Manager.CameraService.IsCameraAvailable)
            {
                await App.Current.Manager.CameraService.InitialAsync(hiddenCaptureElement, null, true);
                await App.Current.Manager.CameraService.StartPreviewAsync();
            }
            App.Current.OnAppResumed += Current_OnAppResumed;
            base.OnNavigatedTo(e);
        }

        private async void Current_OnAppResumed()
        {
            await App.Current.Manager.CameraService.InitialAsync(hiddenCaptureElement, null, true);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            App.Current.OnAppResumed -= Current_OnAppResumed;
        }

        bool flashEnabled = false;
        private void Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            flashEnabled = !flashEnabled;
            App.Current.Manager.CameraService.SetFlashStatus(flashEnabled);
        }
    }
}
