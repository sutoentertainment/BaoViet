using BaoViet.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace BaoViet.Models
{
    public class Paper : BaseModel
    {
        public string Title { get; set; }
        public string ImageSource { get; set; }

        public ObservableCollection<Category> Categories { get; set; }

        public PaperType Type;
        public string HomePage;

        #region UI Property

        public Thickness Margin { get; set; }
        public double CellWidth { get; set; }

        #endregion


        public int Index = 0;

        public Paper()
        {
            this.Title = "";
            this.ImageSource = "";
            if(App.IsMobile)
                this.CellWidth = ((WindowsSize.Width - 10 * 4) / 3);
            else
                this.CellWidth = ((500 - 10 * 4) / 2);
            Categories = new ObservableCollection<Category>();
        }
    }
}
