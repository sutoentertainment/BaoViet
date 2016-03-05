using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace BaoVietCore.Models
{
    public class DispatcherTimerExt
    {
        public DispatcherTimer Timer;
        public string Id;

        public bool IsEnabled
        {
            get
            {
                return Timer.IsEnabled;
            }
        }
        public TimeSpan Interval
        {
            get
            {
                return Timer.Interval;
            }
            set
            {
                Timer.Interval = value;
            }
        }

        public DispatcherTimerExt()
        {
            Timer = new DispatcherTimer();
            Id = Guid.NewGuid().ToString();
        }

        public void Start()
        {
            Timer.Start();
        }

        public void Stop()
        {
            Timer.Stop();
        }
    }
}
