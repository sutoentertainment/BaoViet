using BaoViet.DataContext;
using BaoViet.Helpers;
using BaoViet.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace BaoViet.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ContainerMaster : Page
    {

        public RootDataContext ViewModel { get; set; }

        public ContainerMaster()
        {
            this.InitializeComponent();
            App.MasterFrame = MasterFrame;
            ViewModel = App.RootDataContext;
            this.DataContext = ViewModel;

            App.MasterFrame.Navigate(typeof(Home_Page));
            App.OnToastRise += App_OnToastActivated;
            SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
            Window.Current.CoreWindow.KeyUp += CoreWindow_KeyUp;
        }

        bool IsAltKeyPressed = false;

        private void CoreWindow_KeyUp(CoreWindow sender, KeyEventArgs e)
        {
            if (e.VirtualKey == Windows.System.VirtualKey.Menu)
                IsAltKeyPressed = false;
        }

        private void CoreWindow_KeyDown(CoreWindow sender, KeyEventArgs e)
        {
            //if (e.VirtualKey == Windows.System.VirtualKey.Menu)
            //    IsAltKeyPressed = true;
            //else
            {
                if (e.VirtualKey == Windows.System.VirtualKey.Back)
                {
                    //TODO: Raise backNavigation();
                    //App.InvokeOnBackRequested();

                    OnBackRequested(null, null);
                }

                if (e.VirtualKey == Windows.System.VirtualKey.F5)
                {
                    App.InvokeOnRefreshRequested();
                }
            }
        }

        private void ToastTapped(object sender, TappedRoutedEventArgs e)
        {
            App.InvokeOnToastRise("Sync complete", 2500);

            TileManager tileManager = new TileManager();
        }

        private void OnBackRequested(object sender, BackRequestedEventArgs e)
        {
            if (App.MasterFrame == null)
            {
                return;
            }

            if (App.MasterFrame.CanGoBack)
            {
                App.MasterFrame.GoBack();
                if (e != null)
                    e.Handled = true;
            }
        }


        //private void App_OnToastActivated(string text, double milisecs)

        double pmilisecs;
        private void App_OnToastActivated(string text, double milisecs)
        {
            pmilisecs = milisecs;
            //TODO: Make toast independent
            NotificationContainer.Visibility = Visibility.Visible;
            NotificationText.Text = text;
            DoubleAnimation animation = new DoubleAnimation();
            animation.From = NotificationContainer.Opacity;
            animation.To = 1;
            animation.Duration = TimeSpan.FromMilliseconds(300);
            QuadraticEase ease = new QuadraticEase();
            ease.EasingMode = EasingMode.EaseOut;
            animation.EasingFunction = ease;

            Storyboard storyboard = new Storyboard();
            Storyboard.SetTarget(animation, NotificationContainer);
            Storyboard.SetTargetProperty(animation, "Opacity");

            DoubleAnimation translate = new DoubleAnimation();
            translate.From = NotificationTransform.X;
            translate.To = -20;
            translate.Duration = TimeSpan.FromMilliseconds(300);
            translate.EasingFunction = ease;

            Storyboard.SetTarget(translate, NotificationTransform);
            Storyboard.SetTargetProperty(translate, "X");
            storyboard.Children.Add(translate);

            storyboard.Children.Add(animation);
            storyboard.Completed += StartTimer;
            storyboard.Begin();


        }

        List<DispatcherTimerExt> Timers = new List<DispatcherTimerExt>();

        private void StartTimer(object sender, object e)
        {
            //Start hiding timer
            DispatcherTimerExt NotificationTimer = new DispatcherTimerExt();
            NotificationTimer.Timer.Interval = TimeSpan.FromMilliseconds(pmilisecs);
            NotificationTimer.Timer.Tick += NotificationTimer_Tick;
            NotificationTimer.Timer.Start();
            NotificationContainer.DataContext = NotificationTimer;
            Timers.Add(NotificationTimer);
        }

        private void NotificationTimer_Tick(object sender, object e)
        {
            DoubleAnimation animation = new DoubleAnimation();
            animation.From = NotificationContainer.Opacity;
            animation.To = 0;
            animation.Duration = TimeSpan.FromMilliseconds(300);
            QuadraticEase ease = new QuadraticEase();
            ease.EasingMode = EasingMode.EaseIn;
            animation.EasingFunction = ease;

            DoubleAnimation translate = new DoubleAnimation();
            translate.From = NotificationTransform.X;
            translate.To = 20;
            translate.Duration = TimeSpan.FromMilliseconds(300);
            translate.EasingFunction = ease;

            Storyboard storyboard = new Storyboard();
            Storyboard.SetTarget(animation, NotificationContainer);
            Storyboard.SetTargetProperty(animation, "Opacity");

            Storyboard.SetTarget(translate, NotificationTransform);
            Storyboard.SetTargetProperty(translate, "X");
            storyboard.Children.Add(translate);

            storyboard.Children.Add(animation);
            storyboard.Completed += Storyboard_Completed;
            storyboard.Begin();

            if (sender != null)
            {
                DispatcherTimer NotificationTimer = sender as DispatcherTimer;
                if (NotificationTimer.IsEnabled)
                    NotificationTimer.Stop();

            }
            else
            {
                var timer = NotificationContainer.DataContext as DispatcherTimer;
                if (timer != null)
                {
                    if (timer.IsEnabled)
                        timer.Stop();
                    Timers.Clear();
                }
            }

        }

        private void Storyboard_Completed(object sender, object e)
        {
            NotificationContainer.Visibility = Visibility.Collapsed;
        }

        private void MasterFrame_Navigated(object sender, NavigationEventArgs e)
        {

        }

        private void DissmisToast(object sender, TappedRoutedEventArgs e)
        {
            NotificationTimer_Tick(null, null);
        }
    }
}
