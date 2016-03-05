using BaoViet.Interfaces;
using BaoVietCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Connectivity;
using Windows.UI.Xaml;
using Windows.ApplicationModel.Activation;

namespace BaoViet
{
    public class SettingKey
    {
        public const string LockRotation = "LockRotation";
    }
    public class ApplicationBase : Application, IApp
    {
        /// <summary>
        /// React to network status changed
        /// </summary>
        public NetworkStatusChangedEventHandler networkStatusCallback { get; set; }
        public Manager Manager { get; set; }


        public event OnToastActivatedEventHandler OnToastRise;


        public event OnToastTappedEventHandler OnToastTapped;


        public event OnProtocolActivatedEventHandler OnProtocolActivated;


        public event OnBackRequestedEventHandler OnBackRequested;


        public event OnRefreshRequestedEventHandler OnRefreshRequested;


        public event OnAppResumeEventHandler OnAppResumed;

        public void InvokeOnBackRequested()
        {
            if (OnBackRequested != null)
                OnBackRequested.Invoke();
        }

        internal void InvokeOnRefreshRequested()
        {
            if (OnRefreshRequested != null)
                OnRefreshRequested.Invoke();
        }

        public void InvokeOnToastTapped(ToastAction action)
        {
            if (OnToastTapped != null)
                OnToastTapped.Invoke(action);
        }

        public void InvokeToast(string text, double milisecs)
        {
            if (OnToastRise != null)
                OnToastRise.Invoke(text, milisecs);
        }

        protected override void OnActivated(IActivatedEventArgs e)
        {
            if (e.Kind == ActivationKind.Protocol)
            {
                var protocolArgs = e as ProtocolActivatedEventArgs;
                var uri = protocolArgs.Uri;

                if (OnProtocolActivated != null)
                    OnProtocolActivated.Invoke(uri.ToString());
            }
            base.OnActivated(e);
        }


        public void App_Resuming(object sender, object e)
        {
            if (OnAppResumed != null)
                OnAppResumed.Invoke();
        }
    }
}
