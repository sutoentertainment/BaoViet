using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Connectivity;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace BaoViet.VisualStateTriggers
{
    public class NetworkConnectionTrigger : StateTriggerBase
    {
        private bool requiresInternet;

        public NetworkConnectionTrigger()
        {
            NetworkInformation.NetworkStatusChanged += NetworkInformationOnNetworkStatusChanged;
        }

        public bool RequiresInternet
        {
            get { return this.requiresInternet; }
            set
            {
                this.requiresInternet = value;
                if (NetworkInformation.GetInternetConnectionProfile() != null)
                {
                    SetActive(value);
                }
                else
                {
                    SetActive(!value);
                }
            }
        }

        private async void NetworkInformationOnNetworkStatusChanged(object sender)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                if (NetworkInformation.GetInternetConnectionProfile() != null)
                {
                    SetActive(this.RequiresInternet);
                }
                else
                {
                    SetActive(!this.RequiresInternet);
                }
            });
        }
    }

}
