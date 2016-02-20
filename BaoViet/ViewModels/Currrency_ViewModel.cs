using BaoViet.Interfaces;
using BaoViet.Models;
using BaoVietCore.Interfaces;
using BaoVietCore.Models;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;
using WinUX.Extensions;

namespace BaoViet.ViewModels
{
    public class Currency_ViewModel : ViewModelBase, INavigable, ITrackingAble
    {

        public string ScreenName
        {
            get
            {
                return Localytics.LocalyticsScreen.CurrencyPage;
            }
        }

        public ObservableCollection<CurrencyInfo> CurrencyInfos { get; set; }

        public Currency_ViewModel()
        {
            CurrencyInfos = new ObservableCollection<CurrencyInfo>();
            LoadData();
        }

        public void LoadData()
        {
            var info = new CurrencyInfo();
            info.Code = "USD";
            info.Buying = "120";
            info.Transfer = "110";
            info.Selling = "100";
            CurrencyInfos.Add(info);
        }

        public void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Back)
                return;
        }

        public void OnNavigatedFrom(NavigationEventArgs e)
        {
        }

        public void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {

        }

        public bool AllowGoBack()
        {
            return true;
        }
    }
}
