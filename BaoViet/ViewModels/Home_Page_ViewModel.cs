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

        public ObservableCollection<Paper> FrontPagePaper { get; set; }

        public ObservableCollection<MenuItem> ListMenuItem { get; set; }


        public RelayCommand OpenMenuCommand { get; set; }
        Action OpenMenuAction;

        public RelayCommand GoToPaperToHidePageCommand { get; set; }
        Action GoToPaperToHidePageAction;

        public Home_Page_ViewModel()
        {
            FrontPagePaper = new ObservableCollection<Paper>();
            ListMenuItem = new ObservableCollection<MenuItem>();
            IsTransparentTile = SettingHelper.LoadSetting("TransparentTile") == null ? false : (bool)SettingHelper.LoadSetting("TransparentTile");
            LoadData();
        }

        public void LoadData()
        {
            FrontPagePaper.Add(new Paper() { Title = "VnExpress", Type = PaperType.VnExpress, HomePage = "http://vnexpress.net", ImageSource= "ms-appx:///Assets/Logo/logo-vne.png" });
            FrontPagePaper.Add(new Paper() { Title = "Dân trí", Type = PaperType.DânTrí, HomePage = "http://dantri.com.vn", ImageSource = "ms-appx:///Assets/Logo/logo-dantri.png" });
            FrontPagePaper.Add(new Paper() { Title = "Tiền phong", Type = PaperType.TienPhong, HomePage = "http://www.tienphong.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-tienphong.png" });
            FrontPagePaper.Add(new Paper() { Title = "VietnamPlus", Type = PaperType.VietnamPlus, HomePage = "http://www.vietnamplus.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-vnp.png" });
            
            FrontPagePaper.Add(new Paper() { Title = "Tinh tế", Type = PaperType.TinhTế, HomePage = "http://tinhte.vn", ImageSource = "ms-appx:///Assets/Logo/logo-tinhte.png" });
            FrontPagePaper.Add(new Paper() { Title = "Số hóa", Type = PaperType.SốHóa, HomePage = "http://sohoa.vnexpress.net", ImageSource = "ms-appx:///Assets/Logo/logo-sohoa.png" });
            FrontPagePaper.Add(new Paper() { Title = "Vietnamnet", Type = PaperType.Vietnamnet, HomePage="http://vietnamnet.vn", ImageSource = "ms-appx:///Assets/Logo/logo-vietnamnet.png" });
            FrontPagePaper.Add(new Paper() { Title = "WinphoneViet", Type = PaperType.WinphoneViet, HomePage = "http://winphoneviet.com", ImageSource = "ms-appx:///Assets/Logo/logo-wpv.png" });
            FrontPagePaper.Add(new Paper() { Title = "Báo mới", Type = PaperType.BaoMoi, HomePage = "http://www.baomoi.com/", ImageSource = "ms-appx:///Assets/Logo/logo-baomoi.png" });
            FrontPagePaper.Add(new Paper() { Title = "Ngôi sao", Type = PaperType.NgôiSao, HomePage = "http://ngoisao.net", ImageSource = "ms-appx:///Assets/Logo/logo-ngoisao.png" });
            FrontPagePaper.Add(new Paper() { Title = "Tuổi trẻ", Type = PaperType.TuổiTre, HomePage = "http://tuoitre.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-tuoitre.png" });
            FrontPagePaper.Add(new Paper() { Title = "Game thủ", Type = PaperType.Gamethu, HomePage = "http://gamethu.vnexpress.net/", ImageSource = "ms-appx:///Assets/Logo/logo-gamethu.png" });
            FrontPagePaper.Add(new Paper() { Title = "Genk", Type = PaperType.Genk, HomePage = "http://genk.vn", ImageSource = "ms-appx:///Assets/Logo/logo-genk.png" });
            FrontPagePaper.Add(new Paper() { Title = "Lao động", Type = PaperType.LaoDong, HomePage = "http://laodong.com.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-laodong.png" });
            FrontPagePaper.Add(new Paper() { Title = "Bóng đá", Type = PaperType.BongDa, HomePage = "http://www.bongda.com.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-bongda.png" });
            FrontPagePaper.Add(new Paper() { Title = "Tin thể thao", Type = PaperType.TinTheThao, HomePage = "http://www.tinthethao.com.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-tinthethao.png" });
            FrontPagePaper.Add(new Paper() { Title = "24h", Type = PaperType.Báo24H, HomePage = "http://24h.com.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-24h.png" });
            FrontPagePaper.Add(new Paper() { Title = "Pc World VN", Type = PaperType.PcWorld, HomePage = "http://www.pcworld.com.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-pcworld.png" });
            FrontPagePaper.Add(new Paper() { Title = "Thế giới Game", Type = PaperType.TheGioiGame, HomePage = "http://thegioigame.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-thegioigame.png" });
            FrontPagePaper.Add(new Paper() { Title = "Thông tin công nghệ", Type = PaperType.ThongTinCongNghe, HomePage = "http://www.thongtincongnghe.com/", ImageSource = "ms-appx:///Assets/Logo/logo-ttcn.png" });
            FrontPagePaper.Add(new Paper() { Title = "Quản trị mạng", Type = PaperType.QuanTriMang, HomePage = "http://quantrimang.com/", ImageSource = "ms-appx:///Assets/Logo/logo-quantrimang.png" });
            FrontPagePaper.Add(new Paper() { Title = "Zing news", Type = PaperType.Zing, HomePage = "http://news.zing.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-zing.png" });
            FrontPagePaper.Add(new Paper() { Title = "Gia đình", Type = PaperType.GiaDinh, HomePage = "http://giadinh.net.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-giadinh.png" });
            FrontPagePaper.Add(new Paper() { Title = "ICT news", Type = PaperType.ICTNews, HomePage = "http://www.ictnews.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-ictnews.png" });
            FrontPagePaper.Add(new Paper() { Title = "VnEconomy", Type = PaperType.VnEconomy, HomePage = "http://vneconomy.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-vneconomy.png" });
            FrontPagePaper.Add(new Paper() { Title = "Nhịp cầu đầu tư", Type = PaperType.NhipCauDauTu, HomePage = "http://nhipcaudautu.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-ncdt.png" });
            FrontPagePaper.Add(new Paper() { Title = "BBC tiếng Việt", Type = PaperType.BBC, HomePage = "http://www.bbc.com/vietnamese", ImageSource = "ms-appx:///Assets/Logo/logo-bbc.png" });
            FrontPagePaper.Add(new Paper() { Title = "VOA tiếng Việt", Type = PaperType.VOA, HomePage = "http://www.voatiengviet.com/", ImageSource = "ms-appx:///Assets/Logo/logo-voa.png" });
            FrontPagePaper.Add(new Paper() { Title = "Eva", Type = PaperType.Eva, HomePage = "http://eva.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-eva.png" });
            FrontPagePaper.Add(new Paper() { Title = "Công An Nhân Dân", Type = PaperType.CongAnNhanDan, HomePage = "http://cand.com.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-cand.png" });
            FrontPagePaper.Add(new Paper() { Title = "An ninh thủ đô", Type = PaperType.AnNinhThuDo, HomePage = "http://anninhthudo.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-antd.png" });
            FrontPagePaper.Add(new Paper() { Title = "Quân đội nhân dân", Type = PaperType.QuanDoiNhanDan, HomePage = "http://www.qdnd.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-qdnd.png" });
            FrontPagePaper.Add(new Paper() { Title = "Đất Việt", Type = PaperType.DatViet, HomePage = "http://baodatviet.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-datviet.png" });
            FrontPagePaper.Add(new Paper() { Title = "Web trẻ thơ", Type = PaperType.WebTreTho, HomePage = "http://www.webtretho.com/", ImageSource = "ms-appx:///Assets/Logo/logo-webtretho.png" });
            FrontPagePaper.Add(new Paper() { Title = "Việt báo", Type = PaperType.VietBao, HomePage = "http://vietbao.vn", ImageSource = "ms-appx:///Assets/Logo/logo-vietbao.png" });
            FrontPagePaper.Add(new Paper() { Title = "Petro Times", Type = PaperType.PetroTimes, HomePage = "http://petrotimes.vn", ImageSource = "ms-appx:///Assets/Logo/logo-petro.png" });
            FrontPagePaper.Add(new Paper() { Title = "Đời sống pháp luật", Type = PaperType.DoiSongPhapLuat, HomePage = "http://www.doisongphapluat.com/", ImageSource = "ms-appx:///Assets/Logo/logo-dspl.png" });
            FrontPagePaper.Add(new Paper() { Title = "Kênh 14", Type = PaperType.Kenh14, HomePage = "http://kenh14.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-kenh14.png" });


            ListMenuItem.Add(new MenuItem() { MenuTitle = "Trang nhất", Glyph = "\xE154" });
            //ListMenuItem.Add(new MenuItem() { MenuTitle = "Đã lưu", Glyph = "\xE082" });
            ListMenuItem.Add(new MenuItem() { MenuTitle = "Cài đặt", Glyph = "\xE115" });

            OpenMenuAction = new Action(OpenMenu);
            OpenMenuCommand = new RelayCommand(OpenMenuAction);

            GoToPaperToHidePageAction = new Action(GoToPaperToHidePage);
            GoToPaperToHidePageCommand = new RelayCommand(GoToPaperToHidePageAction);
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
