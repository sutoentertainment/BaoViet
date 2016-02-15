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
            QueryStatus();
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
                BoughAdRemover = App.Current.Manager.IAPService.CheckProduct("Remove_Ads");

                if (!BoughAdRemover && !InDebugMode)
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
