using BaoVietCore.Factory;
using BaoViet.Interfaces;
using BaoViet.Views;
using BaoVietCore.Interfaces;
using BaoVietCore.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ThHelper;
using GalaSoft.MvvmLight.Messaging;
using BaoVietCore.CustomEventArgs;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Navigation;
using BaoVietCore.Helpers;
using Windows.UI.Xaml.Controls;
using Microsoft.Practices.ServiceLocation;
using BaoViet.Controls;
using BaoViet.Models;

namespace BaoViet.ViewModels
{
    public class Home_Page_ViewModel : ViewModelBase, INavigable, ITrackingAble
    {
        public string AppVersion
        {
            get
            {
                return " " + App.Current.Manager.DeviceService.GetAppVersion();
            }
        }

        string _HambugerIcon = "\x2261";
        public string HambugerIcon
        {
            get
            {
                return _HambugerIcon;
            }
            set
            {
                Set(ref _HambugerIcon, value);
            }
        }


        private bool _IsPaneOpen = false;

        public bool IsPaneOpen
        {
            get
            {
                return _IsPaneOpen;
            }
            set
            {
                Set(ref _IsPaneOpen, value);
                if (_IsPaneOpen)
                {
                    HambugerIcon = "\xE096";
                }
                else
                {
                    HambugerIcon = "\x2261";
                }
            }
        }


        private bool _IsTransparentTile = false;

        public bool IsTransparentTile
        {
            get
            {
                return _IsTransparentTile;
            }
            set
            {
                Set(ref _IsTransparentTile, value);
            }
        }

        public ObservableCollection<IPaper> FrontPagePaper { get; set; }

        public ObservableCollection<IMenuItem> ListMenuItem { get; set; }
        public ObservableCollection<IMenuItem> ExtraTools { get; set; }
        public RelayCommand OpenMenuCommand { get; set; }

        public RelayCommand GoToPaperToHidePageCommand { get; set; }

        public RelayCommand<ItemClickEventArgs> PaperClickedCommand { get; set; }

        public string ScreenName
        {
            get
            {
                return Localytics.LocalyticsScreen.HomePage;
            }
        }

        public Home_Page_ViewModel()
        {
            FrontPagePaper = new ObservableCollection<IPaper>();
            ListMenuItem = new ObservableCollection<IMenuItem>();
            ExtraTools = new ObservableCollection<IMenuItem>();

            _IsTransparentTile = SettingHelper.LoadSetting("TransparentTile") == null ? false : (bool)SettingHelper.LoadSetting("TransparentTile");

            CreateCommand();

            LoadData();
        }

        public void CreateCommand()
        {

            OpenMenuCommand = new RelayCommand(OpenMenu);

            GoToPaperToHidePageCommand = new RelayCommand(GoToPaperToHidePage);

            PaperClickedCommand = new RelayCommand<ItemClickEventArgs>(PaperClicked);
        }

        private void PaperClicked(ItemClickEventArgs e)
        {
            var paper = e.ClickedItem as IPaper;
            //App.MasterFrame.Navigate(typeof(Detail_Page), paper.HomePage);
            OpenPaper(paper);
        }

        private async void OpenPaper(IPaper paper)
        {
            var attribute = new Dictionary<string, string>();
            attribute.Add("name", paper.Title);
            App.Current.Manager.TrackingService.TagEvent(Localytics.LocalyticsEvent.OpenPaper, attribute);

            var vm = ViewModelLocator.Get<List_Categories_ViewModel>();
            vm.CurrentPaper = paper;


            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                if (App.Current.Manager.DeviceService.GetAppState() == AppState.Mobile)
                    App.Current.NavigationService.NavigateTo(Pages.List_Categories_Page);
                else
                {
                    App.Current.NavigationService.GoBack(FrameKey.PaneSplitFrame);
                    App.Current.NavigationService.NavigateTo(Pages.List_Categories_Page, null, FrameKey.PaneSplitFrame);
                }
            });
        }

        public void OnNavigatedFrom(NavigationEventArgs e)
        {
            Messenger.Default.Unregister(this);
        }

        public void OnNavigatedTo(NavigationEventArgs e)
        {
            RegisterMessage();
            if (App.Current.Manager.DeviceService.GetAppState() == AppState.Tablet)
                _IsPaneOpen = true;

            if (e.NavigationMode == NavigationMode.Back)
            {
                return;
            }

            if (e.Parameter != null)
            {
                string uri = e.Parameter.ToString();
                string paper = HttpUtility.ParseQueryString(uri).GetValues("paper").FirstOrDefault();

                if (paper != null)
                {
                    PaperType type = (PaperType)Enum.Parse(typeof(PaperType), paper);
                    var target = this.FrontPagePaper.FirstOrDefault(p => p.Type == type);
                    OpenPaper(target);
                }
            }
        }

        ~Home_Page_ViewModel()
        {

        }

        private void RegisterMessage()
        {
            Messenger.Default.Register<PinEventArgs>(this, PinToStart);
        }

        private async void PinToStart(PinEventArgs p)
        {
            await App.Current.TileManager.CreateSecondaryTileAsync(p.Paper.Title, p.Paper.ImageSource, p.Paper.Title, "?paper=" + p.Paper.TypeString);
        }

        public void LoadData()
        {
            FrontPagePaper.Add(PaperFactory.Create(PaperType.VnExpress));
            FrontPagePaper.Add(PaperFactory.Create(PaperType.DânTrí));
            FrontPagePaper.Add(PaperFactory.Create(PaperType.TienPhong));
            FrontPagePaper.Add(PaperFactory.Create(PaperType.VietnamPlus));
            FrontPagePaper.Add(PaperFactory.Create(PaperType.TinhTế));
            FrontPagePaper.Add(PaperFactory.Create(PaperType.SốHóa));
            FrontPagePaper.Add(PaperFactory.Create(PaperType.Vietnamnet));
            FrontPagePaper.Add(PaperFactory.Create(PaperType.WinphoneViet));
            FrontPagePaper.Add(PaperFactory.Create(PaperType.BaoMoi));
            FrontPagePaper.Add(PaperFactory.Create(PaperType.NgôiSao));
            FrontPagePaper.Add(PaperFactory.Create(PaperType.TuổiTre));
            FrontPagePaper.Add(PaperFactory.Create(PaperType.Gamethu));
            FrontPagePaper.Add(PaperFactory.Create(PaperType.Genk));
            FrontPagePaper.Add(PaperFactory.Create(PaperType.LaoDong));
            FrontPagePaper.Add(PaperFactory.Create(PaperType.BongDa));
            FrontPagePaper.Add(PaperFactory.Create(PaperType.TinTheThao));
            FrontPagePaper.Add(PaperFactory.Create(PaperType.Báo24H));
            FrontPagePaper.Add(PaperFactory.Create(PaperType.PcWorld));
            FrontPagePaper.Add(PaperFactory.Create(PaperType.TheGioiGame));
            FrontPagePaper.Add(PaperFactory.Create(PaperType.ThongTinCongNghe));
            FrontPagePaper.Add(PaperFactory.Create(PaperType.QuanTriMang));
            FrontPagePaper.Add(PaperFactory.Create(PaperType.Zing));
            FrontPagePaper.Add(PaperFactory.Create(PaperType.GiaDinh));
            FrontPagePaper.Add(PaperFactory.Create(PaperType.ICTNews));
            FrontPagePaper.Add(PaperFactory.Create(PaperType.VnEconomy));
            FrontPagePaper.Add(PaperFactory.Create(PaperType.NhipCauDauTu));
            FrontPagePaper.Add(PaperFactory.Create(PaperType.BBC));
            FrontPagePaper.Add(PaperFactory.Create(PaperType.VOA));
            FrontPagePaper.Add(PaperFactory.Create(PaperType.Eva));
            FrontPagePaper.Add(PaperFactory.Create(PaperType.CongAnNhanDan));
            FrontPagePaper.Add(PaperFactory.Create(PaperType.AnNinhThuDo));
            FrontPagePaper.Add(PaperFactory.Create(PaperType.QuanDoiNhanDan));


            FrontPagePaper.Add(PaperFactory.Create(PaperType.DatViet));
            FrontPagePaper.Add(PaperFactory.Create(PaperType.WebTreTho));
            FrontPagePaper.Add(PaperFactory.Create(PaperType.VietBao));
            //FrontPagePaper.Add(PaperFactory.Create(PaperType.PetroTimes));
            //FrontPagePaper.Add(PaperFactory.Create(PaperType.DoiSongPhapLuat));
            FrontPagePaper.Add(PaperFactory.Create(PaperType.Kenh14));



            //FrontPagePaper.Add(new VnExpressPaper() { Title = "Petro Times", Type = PaperType.PetroTimes, HomePage = "http://petrotimes.vn", ImageSource = "ms-appx:///Assets/Logo/logo-petro.png" });
            //FrontPagePaper.Add(new VnExpressPaper() { Title = "Đời sống pháp luật", Type = PaperType.DoiSongPhapLuat, HomePage = "http://www.doisongphapluat.com/", ImageSource = "ms-appx:///Assets/Logo/logo-dspl.png" });


            ListMenuItem.Add(new MenuItemBase() { MenuTitle = "Trang nhất", Glyph = "\xE154", Type = MenuItemType.Home });
            ListMenuItem.Add(new MenuItemBase() { MenuTitle = "Đã lưu", Glyph = "\xE082", Type = MenuItemType.Saved });
            ListMenuItem.Add(new MenuItemBase() { MenuTitle = "Cài đặt", Glyph = "\xE115", Type = MenuItemType.Setting });

            ExtraTools.Add(new CurrencyMenuItem(Symbol.Switch) { MenuTitle = "Tra cứu tỉ giá" });
            ExtraTools.Add(new OCRMenuItem(Symbol.Caption) { MenuTitle = "Chuyển ảnh thành chữ" });
            ExtraTools.Add(new WeatherMenuItem(Symbol.World) { MenuTitle = "Thời tiết" });
            ExtraTools.Add(new FlashMenuItem(Symbol.Trim) { MenuTitle = "Flash" });

        }

        private void GoToPaperToHidePage()
        {
            //App.Current.NavigationService.NavigateTo(typeof(PaperToHide_Page));
        }

        private void OpenMenu()
        {
            IsPaneOpen = !IsPaneOpen;
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
