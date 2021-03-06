﻿using BaoViet.ViewModels;
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
            else
                FrameDictionary[framekey] = frame;

            if (framekey == FrameKey.MainFrame)
            {
                MainFrame = frame;
                frame.Navigated += MainFrame_Navigated;
            }
        }


        internal void ConfigurePage(Pages key, Type type)
        {
            if (!PageDictionary.ContainsKey(key))
                PageDictionary.Add(key, type);
            else
                PageDictionary[key] = type;
        }

        public Frame GetFrame(FrameKey framekey)
        {
            if (FrameDictionary.ContainsKey(framekey))
                return FrameDictionary[framekey];
            return null;
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

        public void GoBack(FrameKey framekey, bool recursive = false)
        {
            if (FrameDictionary.ContainsKey(framekey))
                if (recursive)
                    while (FrameDictionary[framekey].CanGoBack)
                        FrameDictionary[framekey].GoBack();
                else
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

        public bool FrameAvailable(FrameKey framekey)
        {
            return FrameDictionary.ContainsKey(framekey);
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


    public enum FrameKey
    {
        MainFrame,
        RootFrame,
        MenuFrame,
        PaneSplitFrame,
        CurrentFrame,
    }
}
