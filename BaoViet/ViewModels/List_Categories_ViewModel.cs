using BaoViet.Interfaces;
using BaoVietCore.Interfaces;
using BaoVietCore.Models;
using GalaSoft.MvvmLight;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;

namespace BaoViet.ViewModels
{
    public class List_Categories_ViewModel : ViewModelBase, INavigable
    {
        IPaper _CurrentPaper;
        public IPaper CurrentPaper
        {
            get
            {
                return _CurrentPaper;
            }
            set
            {
                Set(ref _CurrentPaper, value);
            }
        }

        bool _HeaderLoaded = false;
        public bool HeaderLoaded
        {
            get
            {
                return _HeaderLoaded;
            }
            set
            {
                Set(ref _HeaderLoaded, value);
            }
        }

        public List_Categories_ViewModel()
        {

        }

        public async void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Back)
                return;
            HeaderLoaded = true;
            //VisualStateManager.GoToState(this, "HeaderLoaded", true);
            if (CurrentPaper.Categories.Count == 0)
            {
                var feed = new FeedItem();
                feed.Link = CurrentPaper.HomePage;
                var detail = ServiceLocator.Current.GetInstance<Detail_ViewModel>();
                detail.CurrentFeed = feed;

                await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    App.Current.NavigationService.NavigateTo(Pages.DetailPage);
                });
            }
        }

        public void OnNavigatedFrom(NavigationEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Back)
            {
                HeaderLoaded = false;
            }
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
