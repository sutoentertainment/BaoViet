using BaoViet.Interfaces;
using BaoViet.Services;
using BaoVietCore.Helpers;
using BaoVietCore.Interfaces;
using BaoVietCore.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace BaoViet.ViewModels
{
    public class List_Categories_ViewModel : ViewModelBase, INavigable, ITrackingAble
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

        public RelayCommand<ItemClickEventArgs> CategoryClickedCommand { get; set; }

        public string ScreenName
        {
            get
            {
                return Localytics.LocalyticsScreen.ListCategoryPage;
            }
        }

        public List_Categories_ViewModel()
        {
            CategoryClickedCommand = new RelayCommand<ItemClickEventArgs>(CategoryClicked);
        }

        private void CategoryClicked(ItemClickEventArgs e)
        {
            var cate = e.ClickedItem as Category;
            OpenCategory(cate);
        }

        private async void OpenCategory(Category cate)
        {
            var attribute = new Dictionary<string, string>();
            attribute.Add("paper name", cate.Owner.Title);
            attribute.Add("name", cate.Title);
            App.Current.Manager.TrackingService.TagEvent(Localytics.LocalyticsEvent.OpenCategory, attribute);

            var vm = ViewModelLocator.Get<List_Articles_ViewModel>();
            vm.CurrentCategory = cate;

            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                if(DeviceHelper.GetAppState() == AppState.Mobile)
                    App.Current.NavigationService.NavigateTo(Pages.List_Articles_Page);
                else
                    App.Current.NavigationService.NavigateTo(Pages.List_Articles_Page, null, FrameKey.PaneSplitFrame);
            });
        }

        public async void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Back)
            {
                //Clear frame cache
                App.Current.NavigationService.ClearCache(FrameKey.MainFrame);

                if (CurrentPaper.Categories.Count <= 1)

                    await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                    {
                        App.Current.NavigationService.GoBack();
                    });
                return;
            }
            HeaderLoaded = true;
            //VisualStateManager.GoToState(this, "HeaderLoaded", true);
            if (CurrentPaper.Categories.Count == 0)
            {
                var feed = new FeedItem();
                feed.Link = CurrentPaper.HomePage;
                var detail = ViewModelLocator.Get<Detail_ViewModel>();
                detail.CurrentFeed = feed;

                await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    if (DeviceHelper.GetAppState() == AppState.Mobile)
                        App.Current.NavigationService.NavigateTo(Pages.DetailPage);
                    else
                        App.Current.NavigationService.NavigateTo(Pages.DetailPage, null, FrameKey.PaneSplitFrame);
                });
            }
            else if (CurrentPaper.Categories.Count == 1)
            {
                OpenCategory(CurrentPaper.Categories[0]);
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
