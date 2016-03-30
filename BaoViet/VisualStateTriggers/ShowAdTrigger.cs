using BaoViet.IAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace BaoViet.VisualStateTriggers
{
    public class ShowAdTrigger : StateTriggerBase
    {
        public bool ShowAd { get; set; }

        public ShowAdTrigger()
        {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            QueryStatus();
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        }


        private async Task QueryStatus()
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                var InDebugMode = false;
                var BoughAdRemover = false;
                var showAd = false;
#if DEBUG
                InDebugMode = true;
#endif
                BoughAdRemover = App.Current.Manager.IAPService.CheckProduct(IAPItem.ADS_REMOVAL_ID);

                if (!BoughAdRemover || InDebugMode)
                    showAd = true;
                else
                    showAd = false;

                if (ShowAd == showAd)
                    SetActive(true);
                else
                    SetActive(false);

            });
        }
    }
}
