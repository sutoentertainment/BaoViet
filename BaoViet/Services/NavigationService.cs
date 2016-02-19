using BaoViet.ViewModels;
using BaoViet.Views;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace BaoViet.Services
{
    public class NavigationService
    {
        protected Dictionary<Pages, Type> PageDictionary = new Dictionary<Pages, Type>();
        protected Dictionary<FrameKey, Frame> FrameDictionary = new Dictionary<FrameKey, Frame>();
        protected Frame MainFrame;


        public void Configure(FrameKey framekey, Frame frame)
        {
            if (!FrameDictionary.ContainsKey(framekey))
                FrameDictionary.Add(framekey, frame);
            if (framekey == FrameKey.MainFrame)
            {
                MainFrame = frame;
                frame.Navigated += MainFrame_Navigated;
            }
        }

        public void ConfigPage()
        {
            PageDictionary.Add(Pages.Container, typeof(ContainerMaster));
            PageDictionary.Add(Pages.HomePage, typeof(Home_Page));
            PageDictionary.Add(Pages.DetailPage, typeof(Detail_Page));
            PageDictionary.Add(Pages.List_Categories_Page, typeof(List_Categories_Page));
            PageDictionary.Add(Pages.List_Articles_Page, typeof(List_Articles_Page));
            PageDictionary.Add(Pages.Saved_Articles_Page, typeof(Saved_Articles_Page));
            PageDictionary.Add(Pages.Currency, typeof(Currency_Page));
        }

        private async void MainFrame_Navigated(object sender, Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                            //App.MasterFrame.CanGoBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
                            MainFrame.BackStackDepth == 0 ? AppViewBackButtonVisibility.Collapsed : AppViewBackButtonVisibility.Visible;
            });
        }

        public void GoBack()
        {
            if (MainFrame.CanGoBack)
                MainFrame.GoBack();
        }

        public void GoBack(FrameKey framekey)
        {
            if (FrameDictionary.ContainsKey(framekey))
                while (FrameDictionary[framekey].CanGoBack)
                    FrameDictionary[framekey].GoBack();
        }

        public bool CanGoBack()
        {
            return MainFrame.CanGoBack;
        }
        public bool CanGoBack(FrameKey framekey)
        {
            if (FrameDictionary.ContainsKey(framekey))
                return FrameDictionary[framekey].CanGoBack;
            return true;
        }

        public virtual void NavigateTo(Pages page, object parameter = null, FrameKey framekey = FrameKey.MainFrame)
        {
            Debug.WriteLine(page.ToString());
            FrameDictionary[framekey].Navigate(PageDictionary[page], parameter);
        }

        public void ClearCache(FrameKey framekey)
        {
            var size = FrameDictionary[framekey].CacheSize;
            FrameDictionary[framekey].CacheSize = 0;
            FrameDictionary[framekey].CacheSize = size;
        }
    }
}
