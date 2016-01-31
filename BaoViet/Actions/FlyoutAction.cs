using Microsoft.Xaml.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace BaoViet.Actions
{
    public class CloseFlyoutAction : DependencyObject, IAction
    {
        public object Execute(object sender, object parameter)
        {
            var senderElement = sender as FrameworkElement;
            FlyoutBase flyoutBase = FlyoutBase.GetAttachedFlyout(senderElement);
            flyoutBase.Hide();
            return null;
        }
    }

    public class OpenFlyoutAction : DependencyObject, IAction
    {
        public object Execute(object sender, object parameter)
        {
            var senderElement = sender as FrameworkElement;
            FlyoutBase flyoutBase = FlyoutBase.GetAttachedFlyout(senderElement);
            flyoutBase.ShowAt(senderElement);
            return null;
        }
    }
}
