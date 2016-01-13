using BaoViet.Interfaces;
using BaoViet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoViet.ViewModels
{
    public class List_Categories_ViewModel : BaseModel
    {
        IPaper _CurrentPaper;
        public IPaper CurrentPaper
        {
            get
            {
                return _CurrentPaper;
            }
            set
            {
                _CurrentPaper = value;
                RaisePropertyChanged("CurrentPaper");
            }
        }

        public List_Categories_ViewModel()
        {

        }
    }
}
