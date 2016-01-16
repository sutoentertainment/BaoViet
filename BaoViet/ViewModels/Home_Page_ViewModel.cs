using BaoViet.Factory;
using BaoViet.Interfaces;
using BaoViet.Models;
using BaoViet.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ThHelper;

namespace BaoViet.ViewModels
{
    public class Home_Page_ViewModel : BaseModel
    {

        string _HambugerIcon = "\x2261";
        public string HambugerIcon
        {
            get
            {
                return _HambugerIcon;
            }
            set
            {
                _HambugerIcon = value;
                RaisePropertyChanged("HambugerIcon");
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
                _IsPaneOpen = value;
                if (_IsPaneOpen)
                {
                    HambugerIcon = "\xE096";
                }
                else
                {
                    HambugerIcon = "\x2261";
                }
                RaisePropertyChanged("IsPaneOpen");
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
                _IsTransparentTile = value;
                RaisePropertyChanged("Property");
            }
        }

        public ObservableCollection<IPaper> FrontPagePaper { get; set; }

        public ObservableCollection<MenuItem> ListMenuItem { get; set; }


        public RelayCommand OpenMenuCommand { get; set; }

        public RelayCommand GoToPaperToHidePageCommand { get; set; }

        public Home_Page_ViewModel()
        {
            FrontPagePaper = new ObservableCollection<IPaper>();
            ListMenuItem = new ObservableCollection<MenuItem>();
            IsTransparentTile = SettingHelper.LoadSetting("TransparentTile") == null ? false : (bool)SettingHelper.LoadSetting("TransparentTile");
            LoadData();
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

            //FrontPagePaper.Add(new VnExpressPaper() { Title = "Ngôi sao", Type = PaperType.NgôiSao, HomePage = "http://ngoisao.net", ImageSource = "ms-appx:///Assets/Logo/logo-ngoisao.png" });
            //FrontPagePaper.Add(new VnExpressPaper() { Title = "Tuổi trẻ", Type = PaperType.TuổiTre, HomePage = "http://tuoitre.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-tuoitre.png" });
            //FrontPagePaper.Add(new VnExpressPaper() { Title = "Game thủ", Type = PaperType.Gamethu, HomePage = "http://gamethu.vnexpress.net/", ImageSource = "ms-appx:///Assets/Logo/logo-gamethu.png" });
            //FrontPagePaper.Add(new VnExpressPaper() { Title = "Genk", Type = PaperType.Genk, HomePage = "http://genk.vn", ImageSource = "ms-appx:///Assets/Logo/logo-genk.png" });
            //FrontPagePaper.Add(new VnExpressPaper() { Title = "Lao động", Type = PaperType.LaoDong, HomePage = "http://laodong.com.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-laodong.png" });
            //FrontPagePaper.Add(new VnExpressPaper() { Title = "Bóng đá", Type = PaperType.BongDa, HomePage = "http://www.bongda.com.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-bongda.png" });
            //FrontPagePaper.Add(new VnExpressPaper() { Title = "Tin thể thao", Type = PaperType.TinTheThao, HomePage = "http://www.tinthethao.com.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-tinthethao.png" });
            //FrontPagePaper.Add(new VnExpressPaper() { Title = "24h", Type = PaperType.Báo24H, HomePage = "http://24h.com.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-24h.png" });
            //FrontPagePaper.Add(new VnExpressPaper() { Title = "Pc World VN", Type = PaperType.PcWorld, HomePage = "http://www.pcworld.com.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-pcworld.png" });
            //FrontPagePaper.Add(new VnExpressPaper() { Title = "Thế giới Game", Type = PaperType.TheGioiGame, HomePage = "http://thegioigame.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-thegioigame.png" });
            //FrontPagePaper.Add(new VnExpressPaper() { Title = "Thông tin công nghệ", Type = PaperType.ThongTinCongNghe, HomePage = "http://www.thongtincongnghe.com/", ImageSource = "ms-appx:///Assets/Logo/logo-ttcn.png" });
            //FrontPagePaper.Add(new VnExpressPaper() { Title = "Quản trị mạng", Type = PaperType.QuanTriMang, HomePage = "http://quantrimang.com/", ImageSource = "ms-appx:///Assets/Logo/logo-quantrimang.png" });
            //FrontPagePaper.Add(new VnExpressPaper() { Title = "Zing news", Type = PaperType.Zing, HomePage = "http://news.zing.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-zing.png" });
            //FrontPagePaper.Add(new VnExpressPaper() { Title = "Gia đình", Type = PaperType.GiaDinh, HomePage = "http://giadinh.net.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-giadinh.png" });
            //FrontPagePaper.Add(new VnExpressPaper() { Title = "ICT news", Type = PaperType.ICTNews, HomePage = "http://www.ictnews.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-ictnews.png" });
            //FrontPagePaper.Add(new VnExpressPaper() { Title = "VnEconomy", Type = PaperType.VnEconomy, HomePage = "http://vneconomy.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-vneconomy.png" });
            //FrontPagePaper.Add(new VnExpressPaper() { Title = "Nhịp cầu đầu tư", Type = PaperType.NhipCauDauTu, HomePage = "http://nhipcaudautu.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-ncdt.png" });
            //FrontPagePaper.Add(new VnExpressPaper() { Title = "BBC tiếng Việt", Type = PaperType.BBC, HomePage = "http://www.bbc.com/vietnamese", ImageSource = "ms-appx:///Assets/Logo/logo-bbc.png" });
            //FrontPagePaper.Add(new VnExpressPaper() { Title = "VOA tiếng Việt", Type = PaperType.VOA, HomePage = "http://www.voatiengviet.com/", ImageSource = "ms-appx:///Assets/Logo/logo-voa.png" });
            //FrontPagePaper.Add(new VnExpressPaper() { Title = "Eva", Type = PaperType.Eva, HomePage = "http://eva.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-eva.png" });
            //FrontPagePaper.Add(new VnExpressPaper() { Title = "Công An Nhân Dân", Type = PaperType.CongAnNhanDan, HomePage = "http://cand.com.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-cand.png" });
            //FrontPagePaper.Add(new VnExpressPaper() { Title = "An ninh thủ đô", Type = PaperType.AnNinhThuDo, HomePage = "http://anninhthudo.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-antd.png" });
            //FrontPagePaper.Add(new VnExpressPaper() { Title = "Quân đội nhân dân", Type = PaperType.QuanDoiNhanDan, HomePage = "http://www.qdnd.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-qdnd.png" });
            //FrontPagePaper.Add(new VnExpressPaper() { Title = "Đất Việt", Type = PaperType.DatViet, HomePage = "http://baodatviet.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-datviet.png" });
            //FrontPagePaper.Add(new VnExpressPaper() { Title = "Web trẻ thơ", Type = PaperType.WebTreTho, HomePage = "http://www.webtretho.com/", ImageSource = "ms-appx:///Assets/Logo/logo-webtretho.png" });
            //FrontPagePaper.Add(new VnExpressPaper() { Title = "Việt báo", Type = PaperType.VietBao, HomePage = "http://vietbao.vn", ImageSource = "ms-appx:///Assets/Logo/logo-vietbao.png" });
            //FrontPagePaper.Add(new VnExpressPaper() { Title = "Petro Times", Type = PaperType.PetroTimes, HomePage = "http://petrotimes.vn", ImageSource = "ms-appx:///Assets/Logo/logo-petro.png" });
            //FrontPagePaper.Add(new VnExpressPaper() { Title = "Đời sống pháp luật", Type = PaperType.DoiSongPhapLuat, HomePage = "http://www.doisongphapluat.com/", ImageSource = "ms-appx:///Assets/Logo/logo-dspl.png" });
            //FrontPagePaper.Add(new VnExpressPaper() { Title = "Kênh 14", Type = PaperType.Kenh14, HomePage = "http://kenh14.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-kenh14.png" });


            ListMenuItem.Add(new MenuItem() { MenuTitle = "Trang nhất", Glyph = "\xE154" });
            //ListMenuItem.Add(new MenuItem() { MenuTitle = "Đã lưu", Glyph = "\xE082" });
            ListMenuItem.Add(new MenuItem() { MenuTitle = "Cài đặt", Glyph = "\xE115" });
            
            OpenMenuCommand = new RelayCommand(OpenMenu);
            
            GoToPaperToHidePageCommand = new RelayCommand(GoToPaperToHidePage);
    }

        private void GoToPaperToHidePage()
        {
            App.MasterFrame.Navigate(typeof(PaperToHide_Page));
        }

        private void OpenMenu()
        {

        }
    }
}
