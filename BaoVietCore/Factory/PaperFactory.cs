using BaoVietCore.Interfaces;
using BaoVietCore.Models;
using BaoVietCore.Models.Paper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoVietCore.Factory
{
    public class PaperFactory
    {
        public static IPaper Create(PaperType type)
        {
            PaperBase paper;
            switch (type)
            {
                case PaperType.VnExpress:
                    paper = new VnExpressPaper(type);
                    break;
                case PaperType.DânTrí:
                    paper = new DanTriPaper(type);
                    break;
                case PaperType.TinhTế:
                    paper = new TinhTePaper(type);
                    break;
                case PaperType.VietnamPlus:
                    paper = new VietnamPlusPaper(type);
                    break;
                case PaperType.TienPhong:
                    paper = new TienPhongPaper(type);
                    break;
                case PaperType.WinphoneViet:
                    paper = new WinphoneVietPaper(type);
                    break;
                case PaperType.SốHóa:
                    paper = new SoHoaPaper(type);
                    break;
                case PaperType.BongDa:
                    paper = new BongDaPaper(type);
                    break;
                case PaperType.TinTheThao:
                    paper = new TinTheThaoPaper(type);
                    break;
                case PaperType.BaoMoi:
                    paper = new BaoMoiPaper(type);
                    break;
                case PaperType.NgôiSao:
                    paper = new NgoiSaoPaper(type);
                    break;
                case PaperType.TuổiTre:
                    paper = new TuoiTrePaper(type);
                    break;
                case PaperType.Gamethu:
                    paper = new GameThuPaper(type);
                    break;
                case PaperType.Genk:
                    paper = new GenkPaper(type);
                    break;
                case PaperType.LaoDong:
                    paper = new LaoDongPaper(type);
                    break;
                case PaperType.Vietnamnet:
                    paper = new VietnamnetPaper(type);
                    break;
                case PaperType.Báo24H:
                    paper = new Hai4hPaper(type);
                    break;
                case PaperType.PcWorld:
                    paper = new PcWorldPaper(type);
                    break;
                case PaperType.TheGioiGame:
                    paper = new TheGioiGamePaper(type);
                    break;
                case PaperType.ThongTinCongNghe:
                    paper = new ThongTinCongNghePaper(type);
                    break;
                case PaperType.QuanTriMang:
                    paper = new QuanTriMangPaper(type);
                    break;
                case PaperType.Zing:
                    paper = new ZingPaper(type);
                    break;
                case PaperType.GiaDinh:
                    paper = new GiaDinhPaper(type);
                    break;
                case PaperType.ICTNews:
                    paper = new ICTNewsPaper(type);
                    break;
                case PaperType.VnEconomy:
                    paper = new VnEconomyPaper(type);
                    break;
                case PaperType.WebTreTho:
                    paper = new WebTreThoPaper(type);
                    break;
                default:
                    paper = new UnknowPaper(type);
                    break;
            }

            return paper;
        }
    }
}
