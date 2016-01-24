using BaoVietCore;
using Windows.Networking.Connectivity;
using Windows.UI.Xaml.Markup;

namespace BaoViet.Interfaces
{
    interface IApp
    {
        Manager Manager { get; set; }
        NetworkStatusChangedEventHandler networkStatusCallback { get; set; }

        event App.OnAppResumeEventHandler OnAppResumed;
        event App.OnBackRequestedEventHandler OnBackRequested;
        event App.OnProtocolActivatedEventHandler OnProtocolActivated;
        event App.OnRefreshRequestedEventHandler OnRefreshRequested;
        event App.OnToastActivatedEventHandler OnToastRise;
        event App.OnToastTappedEventHandler OnToastTapped;

        void InvokeOnBackRequested();
        void InvokeOnToastRise(string text, double milisec);
        void InvokeOnToastTapped(ToastAction action);
        void OnToastActivated_Invoke(string text, double milisecs);
    }
}