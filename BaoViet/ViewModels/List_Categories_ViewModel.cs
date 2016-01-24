using BaoViet.Interfaces;
using BaoVietCore.Interfaces;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoViet.ViewModels
{
    public class List_Categories_ViewModel : ViewModelBase
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
                Set(ref _CurrentPaper, value);
            }
        }

        bool _HeaderLoaded = false;
        public bool HeaderLoaded
        {
            get
            {
                return _HeaderLoaded;
            }
            set
            {
                Set(ref _HeaderLoaded, value);
            }
        }

        public List_Categories_ViewModel()
        {

        }
    }
}
