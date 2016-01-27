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
            SimpleIoc.Default.Register<Home_Page_ViewModel>();
            SimpleIoc.Default.Register<Detail_ViewModel>();
            SimpleIoc.Default.Register<List_Articles_ViewModel>();
            SimpleIoc.Default.Register<List_Categories_ViewModel>();
            SimpleIoc.Default.Register<Saved_Articles_ViewModel>();
            //SimpleIoc.Default.Register<PromotionViewModel>();
            //SimpleIoc.Default.Register<WalletViewModel>();
            //SimpleIoc.Default.Register<SettingViewModel>();
        }

        public Home_Page_ViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<Home_Page_ViewModel>();
            }
        }

        public Detail_ViewModel Detail
        {
            get
            {
                return ServiceLocator.Current.GetInstance<Detail_ViewModel>();
            }
        }

        public List_Articles_ViewModel ListArticles
        {
            get
            {
                return ServiceLocator.Current.GetInstance<List_Articles_ViewModel>();
            }
        }

        public List_Categories_ViewModel ListCategories
        {
            get
            {
                return ServiceLocator.Current.GetInstance<List_Categories_ViewModel>();
            }
        }

        public Saved_Articles_ViewModel SavedArticles
        {
            get
            {
                return ServiceLocator.Current.GetInstance<Saved_Articles_ViewModel>();
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
    }

    public enum FrameKey
    {
        MainFrame,
        RootFrame,
        MenuFrame
    }
}