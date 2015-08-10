using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace BaoViet.Models
{
    public class DispatcherTimerExt
    {
        public DispatcherTimer Timer;
        public string Id;
        public DispatcherTimerExt()
        {
            Timer = new DispatcherTimer();
            Id = Guid.NewGuid().ToString();
        }
    }
}
