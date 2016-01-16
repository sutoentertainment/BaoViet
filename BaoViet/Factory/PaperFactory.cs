using BaoViet.Interfaces;
using BaoViet.Models;
using BaoViet.Models.Paper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoViet.Factory
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
                case PaperType.BaoMoi:
                    paper = new BaoMoiPaper(type);
                    break;
                default:
                    paper = new UnknowPaper(type);
                    break;
            }

            return paper;
        }
    }
}
