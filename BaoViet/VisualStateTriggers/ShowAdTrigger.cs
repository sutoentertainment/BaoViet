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
#if DEBUG
                InDebugMode = true;
#endif

                if (BoughAdRemover || InDebugMode)

                        SetActive(!this.ShowAd);
                else
                        SetActive(this.ShowAd);

            });
        }
    }
}
