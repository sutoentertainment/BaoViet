using BaoVietCore.Interfaces;
using BaoVietCore.Models;
using BaoVietCore.Models.Paper;
using BaoVietCore.Models.XmlParser;
using BaoVietCore.XmlParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoVietCore.Factory
{
    public class XmlParserFactory
    {
        public static IXmlParser Create(PaperType type)
        {
            IXmlParser parser;
            switch (type)
            {
                case PaperType.VnExpress:
                case PaperType.DânTrí:
                case PaperType.TinhTế:

                case PaperType.TienPhong:
                case PaperType.WinphoneViet:
                case PaperType.SốHóa:
                case PaperType.BongDa:
                case PaperType.BaoMoi:
                case PaperType.NgôiSao:
                case PaperType.Gamethu:
                case PaperType.Genk:
                case PaperType.LaoDong:
                case PaperType.Vietnamnet:
                case PaperType.Báo24H:
                case PaperType.TheGioiGame:
                case PaperType.ThongTinCongNghe:
                case PaperType.GiaDinh:
                case PaperType.ICTNews:
                case PaperType.VnEconomy:
                case PaperType.WebTreTho:
                case PaperType.Eva:
                case PaperType.CongAnNhanDan:
                case PaperType.AnNinhThuDo:
                case PaperType.QuanDoiNhanDan:
                case PaperType.NhipCauDauTu:
                case PaperType.Kenh14:
                case PaperType.DatViet:
                case PaperType.VietBao:
                case PaperType.TuổiTre:
                case PaperType.Custom:
                    parser = new StandartParser();
                    break;
                case PaperType.VietnamPlus:
                    parser = new VietnamPlusParser();
                    break;
                case PaperType.Zing:
                    parser = new ZingParser();
                    break;
                case PaperType.TinTheThao:
                    parser = new TinTheThaoParser();
                    break;
                case PaperType.QuanTriMang:
                    parser = new QuanTriMangParser();
                    break;
                case PaperType.PcWorld:
                    parser = new PcWorldParser();
                    break;
                case PaperType.BBC:
                    parser = new BBCParser();
                    break;
                case PaperType.VOA:
                    parser = new VOAParser();
                    break;
                default:
                    parser = new StandartParser();
                    break;
            }

            return parser;
        }
    }
}
