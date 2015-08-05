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
            LoadData();
        }

        public void LoadData()
        {
            FrontPagePaper.Add(new Paper() { Title = "VnExpress", Type = PaperType.VnExpress, HomePage = "http://vnexpress.net", ImageSource= "ms-appx:///Assets/Logo/logo-vne.png" });
            FrontPagePaper.Add(new Paper() { Title = "Dân trí", Type = PaperType.DânTrí, HomePage = "http://dantri.com.vn", ImageSource = "ms-appx:///Assets/Logo/logo-dantri.png" });
            FrontPagePaper.Add(new Paper() { Title = "Tinh tế", Type = PaperType.TinhTế, HomePage = "http://tinhte.vn", ImageSource = "ms-appx:///Assets/Logo/logo-dantri.png" });
            FrontPagePaper.Add(new Paper() { Title = "Số hóa", Type = PaperType.SốHóa, HomePage = "http://sohoa.vnexpress.net", ImageSource = "ms-appx:///Assets/Logo/logo-dantri.png" });
            FrontPagePaper.Add(new Paper() { Title = "Vietnamnet", Type = PaperType.Vietnamnet, HomePage="http://vietnamnet.vn", ImageSource = "ms-appx:///Assets/Logo/logo-dantri.png" });
            FrontPagePaper.Add(new Paper() { Title = "WinphoneViet", Type = PaperType.WinphoneViet, HomePage = "http://winphoneviet.com", ImageSource = "ms-appx:///Assets/Logo/logo-wpv.png" });
            FrontPagePaper.Add(new Paper() { Title = "Báo mới", Type = PaperType.BaoMoi, HomePage = "http://www.baomoi.com/", ImageSource = "ms-appx:///Assets/Logo/logo-dantri.png" });
            FrontPagePaper.Add(new Paper() { Title = "Ngôi sao", Type = PaperType.NgôiSao, HomePage = "http://ngoisao.net", ImageSource = "ms-appx:///Assets/Logo/logo-dantri.png" });
            FrontPagePaper.Add(new Paper() { Title = "Tuổi trẻ", Type = PaperType.TuổiTre, HomePage = "http://tuoitre.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-dantri.png" });
            FrontPagePaper.Add(new Paper() { Title = "Game thủ", Type = PaperType.Gamethu, HomePage = "http://gamethu.vnexpress.net/", ImageSource = "ms-appx:///Assets/Logo/logo-dantri.png" });
            FrontPagePaper.Add(new Paper() { Title = "Genk", Type = PaperType.Genk, HomePage = "http://genk.vn", ImageSource = "ms-appx:///Assets/Logo/logo-dantri.png" });
            FrontPagePaper.Add(new Paper() { Title = "Lao động", Type = PaperType.LaoDong, HomePage = "http://laodong.com.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-dantri.png" });
            FrontPagePaper.Add(new Paper() { Title = "Bóng đá", Type = PaperType.BongDa, HomePage = "http://www.bongda.com.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-dantri.png" });
            FrontPagePaper.Add(new Paper() { Title = "Tin thể thao", Type = PaperType.TinTheThao, HomePage = "http://www.tinthethao.com.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-dantri.png" });
            FrontPagePaper.Add(new Paper() { Title = "24h", Type = PaperType.Báo24H, HomePage = "http://24h.com.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-dantri.png" });
            FrontPagePaper.Add(new Paper() { Title = "Pc World VN", Type = PaperType.PcWorld, HomePage = "http://www.pcworld.com.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-dantri.png" });
            FrontPagePaper.Add(new Paper() { Title = "Thế giới Game", Type = PaperType.TinhTế, HomePage = "http://www.pcworld.com.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-dantri.png" });
            FrontPagePaper.Add(new Paper() { Title = "Thông tin công nghệ", Type = PaperType.TinhTế, HomePage = "http://www.thongtincongnghe.com/", ImageSource = "ms-appx:///Assets/Logo/logo-dantri.png" });
            FrontPagePaper.Add(new Paper() { Title = "Quản trị mạng", Type = PaperType.TinhTế, HomePage = "http://quantrimang.com/", ImageSource = "ms-appx:///Assets/Logo/logo-dantri.png" });
            FrontPagePaper.Add(new Paper() { Title = "Zing news", Type = PaperType.TinhTế, HomePage = "http://news.zing.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-dantri.png" });
            FrontPagePaper.Add(new Paper() { Title = "Gia đình", Type = PaperType.TinhTế, HomePage = "http://giadinh.net.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-dantri.png" });
            FrontPagePaper.Add(new Paper() { Title = "ICT news", Type = PaperType.TinhTế, HomePage = "http://www.ictnews.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-dantri.png" });
            FrontPagePaper.Add(new Paper() { Title = "VnEconomy", Type = PaperType.TinhTế, HomePage = "http://vneconomy.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-dantri.png" });
            FrontPagePaper.Add(new Paper() { Title = "Nhịp cầu đầu tư", Type = PaperType.TinhTế, HomePage = "http://nhipcaudautu.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-dantri.png" });
            FrontPagePaper.Add(new Paper() { Title = "BBC tiếng Việt", Type = PaperType.TinhTế, HomePage = "http://www.bbc.com/vietnamese", ImageSource = "ms-appx:///Assets/Logo/logo-dantri.png" });
            FrontPagePaper.Add(new Paper() { Title = "VOA tiếng Việt", Type = PaperType.TinhTế, HomePage = "http://www.voatiengviet.com/", ImageSource = "ms-appx:///Assets/Logo/logo-dantri.png" });
            FrontPagePaper.Add(new Paper() { Title = "Eva", Type = PaperType.TinhTế, HomePage = "http://eva.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-dantri.png" });
            FrontPagePaper.Add(new Paper() { Title = "Công An Nhân Dân", Type = PaperType.TinhTế, HomePage = "http://cand.com.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-dantri.png" });
            FrontPagePaper.Add(new Paper() { Title = "An ninh thủ đô", Type = PaperType.TinhTế, HomePage = "http://anninhthudo.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-dantri.png" });
            FrontPagePaper.Add(new Paper() { Title = "Quân đội nhân dân", Type = PaperType.TinhTế, HomePage = "http://www.qdnd.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-dantri.png" });
            FrontPagePaper.Add(new Paper() { Title = "Đất Việt", Type = PaperType.TinhTế, HomePage = "http://baodatviet.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-dantri.png" });
            FrontPagePaper.Add(new Paper() { Title = "Web trẻ thơ", Type = PaperType.TinhTế, HomePage = "http://www.webtretho.com/", ImageSource = "ms-appx:///Assets/Logo/logo-dantri.png" });
            FrontPagePaper.Add(new Paper() { Title = "Việt báo", Type = PaperType.TinhTế, HomePage = "http://vietbao.vn", ImageSource = "ms-appx:///Assets/Logo/logo-dantri.png" });
            FrontPagePaper.Add(new Paper() { Title = "Petro Times", Type = PaperType.TinhTế, HomePage = "http://petrotimes.vn", ImageSource = "ms-appx:///Assets/Logo/logo-dantri.png" });
            FrontPagePaper.Add(new Paper() { Title = "Đời sống pháp luật", Type = PaperType.TinhTế, HomePage = "http://www.doisongphapluat.com/", ImageSource = "ms-appx:///Assets/Logo/logo-dantri.png" });
            FrontPagePaper.Add(new Paper() { Title = "Kênh 14", Type = PaperType.TinhTế, HomePage = "http://kenh14.vn/", ImageSource = "ms-appx:///Assets/Logo/logo-dantri.png" });


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
