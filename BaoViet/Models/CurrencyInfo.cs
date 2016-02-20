using BaoVietCore.Models;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;

namespace BaoViet.Models
{
    public class CurrencyInfo : ObservableObject
    {
        SolidColorBrush _bg;
        public SolidColorBrush Bg
        {
            get { return _bg; }
            set
            {
                Set(ref _bg, value);
            }
        }
        string _code;
        public string Code
        {
            get { return _code; }
            set
            {
                Set(ref _code, value);
            }
        }
        string _transfer;
        public string Transfer
        {
            get { return _transfer; }
            set
            {
                Set(ref _transfer, value);
            }
        }
        string _selling;
        public string Selling
        {
            get { return _selling; }
            set
            {
                Set(ref _selling, value);
            }
        }
        string _buying;
        public string Buying
        {
            get { return _buying; }
            set
            {
                Set(ref _buying, value);
            }
        }

        bool _isHeader = false;
        public bool IsHeader
        {
            get
            {
                return _isHeader;
            }
            set
            {
                Set(ref _isHeader, value);
            }
        }
    }
}
