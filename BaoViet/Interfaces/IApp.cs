using System;
using BaoVietCore;
using Windows.Networking.Connectivity;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml;

namespace BaoViet.Interfaces
{
    interface IApp
    {
        Manager Manager { get; set; }
        NetworkStatusChangedEventHandler networkStatusCallback { get; set; }

        event OnAppResumeEventHandler OnAppResumed;
        event OnBackRequestedEventHandler OnBackRequested;
        event OnProtocolActivatedEventHandler OnProtocolActivated;
        event OnRefreshRequestedEventHandler OnRefreshRequested;
        event OnToastActivatedEventHandler OnToastRise;
        event OnToastTappedEventHandler OnToastTapped;

        void InvokeOnBackRequested();
        void InvokeOnToastRise(string text, double milisec);
        void InvokeOnToastTapped(ToastAction action);
        void OnToastActivated_Invoke(string text, double milisecs);
    }
    public delegate void OnToastActivatedEventHandler(string text, double milisecs);
    public delegate void OnToastTappedEventHandler(ToastAction action);
    public delegate void OnProtocolActivatedEventHandler(string param);
    public delegate void OnBackRequestedEventHandler();
    public delegate void OnRefreshRequestedEventHandler();
    public delegate void OnAppResumeEventHandler();
}