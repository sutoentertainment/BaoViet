/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:ProMe"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using Windows.Phone.UI.Input;

namespace BaoViet.ViewModels
{

    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<IDialogService, DialogService>();
            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            //SimpleIoc.Default.Register<IntroViewModel>();
            SimpleIoc.Default.Register<Home_ViewModel>();
            SimpleIoc.Default.Register<Detail_ViewModel>();
            SimpleIoc.Default.Register<List_Articles_ViewModel>();
            SimpleIoc.Default.Register<List_Categories_ViewModel>();
            SimpleIoc.Default.Register<Saved_Articles_ViewModel>();
            SimpleIoc.Default.Register<Currency_ViewModel>();
            SimpleIoc.Default.Register<Camera_ViewModel>();
            SimpleIoc.Default.Register<MarkDown_ViewModel>();
            //SimpleIoc.Default.Register<PromotionViewModel>();
            //SimpleIoc.Default.Register<WalletViewModel>();
            //SimpleIoc.Default.Register<SettingViewModel>();
        }

        internal static T Get<T>() where T : class
        {
            return SimpleIoc.Default.GetInstance<T>();
        }

        public Camera_ViewModel Camera
        {
            get
            {
                return Get<Camera_ViewModel>();
            }
        }

        public MarkDown_ViewModel MarkDown
        {
            get
            {
                return Get<MarkDown_ViewModel>();
            }
        }

        public Home_ViewModel Main
        {
            get
            {
                return Get<Home_ViewModel>();
            }
        }

        public Detail_ViewModel Detail
        {
            get
            {
                return Get<Detail_ViewModel>();
            }
        }

        public List_Articles_ViewModel ListArticles
        {
            get
            {
                return Get<List_Articles_ViewModel>();
            }
        }

        public List_Categories_ViewModel ListCategories
        {
            get
            {
                return Get<List_Categories_ViewModel>();
            }
        }

        public Saved_Articles_ViewModel SavedArticles
        {
            get
            {
                return Get<Saved_Articles_ViewModel>();
            }
        }

        public Currency_ViewModel Currrency
        {
            get
            {
                return Get<Currency_ViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }

    public enum Pages
    {
        Intro,
        HomePage,
        RestaurantDetailPage,
        Wallet,
        Promotion,
        Setting,
        DetailPage,
        WishlistPage,
        List_Categories_Page,
        List_Articles_Page,
        Container,
        Saved_Articles_Page,
        Currency,
        Flash,
        Gold,
        MarkDown,
    }

    public enum FrameKey
    {
        MainFrame,
        RootFrame,
        MenuFrame,
        PaneSplitFrame
    }
}