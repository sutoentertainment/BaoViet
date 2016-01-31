using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using System.Text.RegularExpressions;
using BaoVietCore.Helpers;
using BaoVietCore.Interfaces;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using BaoVietCore.CustomEventArgs;
using Windows.UI.Xaml.Controls.Primitives;
using GalaSoft.MvvmLight;

namespace BaoVietCore.Models
{
    public abstract class PaperBase : ObservableObject, IPaper
    {
        public virtual ObservableCollection<Category> Categories
        {
            get; set;
        }

        public virtual double CellWidth
        {
            get; set;
        }

        public virtual string HomePage
        {
            get; set;
        }

        public virtual string ImageSource
        {
            get; set;
        }

        public virtual int Index
        {
            get; set;
        }

        public virtual Thickness Margin
        {
            get; set;
        }

        public virtual string Title
        {
            get; set;
        }

        public virtual PaperType Type
        {
            get; set;
        }

        public virtual Task<RssResult> GetFeed(string url)
        {
            throw new NotImplementedException();
        }

        public string TypeString
        {
            get
            {
                return this.Type.ToString();
            }
        }

        public RelayCommand PinCommand
        {
            get; set;
        }

        

        public PaperBase(PaperType type)
        {
            this.Title = "";
            this.ImageSource = "";
            Type = type;
            if (DeviceHelper.CurrentDevice() == DeviceTypes.Mobile)
                this.CellWidth = ((WindowsSize.Width - 10 * 4) / 3);
            else
                this.CellWidth = ((500 - 10 * 4) / 2);
            Categories = new ObservableCollection<Category>();

            PinCommand = new RelayCommand(PinToStart);
        }

        private void PinToStart()
        {
            Messenger.Default.Send<PinEventArgs>(new PinEventArgs(this));
        }

        public string StripHTML(string input)
        {
            return Regex.Replace(input, "<.*?>", String.Empty);
        }
    }
}
