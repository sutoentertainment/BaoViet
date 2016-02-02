using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;

namespace BaoViet.Interfaces
{
    public interface INavigable
    {
        void OnNavigatedTo(NavigationEventArgs e);
        void OnNavigatedFrom(NavigationEventArgs e);
        void OnNavigatingFrom(NavigatingCancelEventArgs e);
        bool AllowGoBack();
    }
}
