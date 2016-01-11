using BaoViet.Interfaces;
using BaoViet.Models;
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
            switch(type)
            {
                case PaperType.VnExpress:
                    paper = new VnExpressPaper(type);
                    break;
                default:
                    paper = new VnExpressPaper(type);
                    break;
            }

            return paper;
        }
    }
}
