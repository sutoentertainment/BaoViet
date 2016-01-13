using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoViet.Models.Paper
{
    public class UnknowPaper : PaperBase
    {
        public UnknowPaper(PaperType type) : base(type)
        {
            this.Title = "Unkow";
        }
    }
}
