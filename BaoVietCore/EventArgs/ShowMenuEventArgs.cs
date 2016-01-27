using BaoVietCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace BaoVietCore.CustomEventArgs
{
    public class ShowMenuEventArgs : EventArgs
    {
        public FrameworkElement Element { get; set; }

        public ShowMenuEventArgs(FrameworkElement e)
        {
            Element = e;
        }
    }
}
