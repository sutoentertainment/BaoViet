using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoVietCore.Models.Paper
{
    public class UnknowPaper : PaperBase
    {
        public UnknowPaper(PaperType type) : base(type)
        {
            this.Title = "Unkow";
            this.HomePage = "http://google.com";
        }
    }
}
