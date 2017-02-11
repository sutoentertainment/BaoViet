using BaoVietCore.Interfaces;
using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoVietCore.Models
{
    [Table("CustomPapers")]
    public class CustomPaper : PaperBase, ISQLItem
    {
        public CustomPaper()
        {

        }

        public CustomPaper(PaperType type) : base(type)
        {
            ImageSource = "ms-appx:///Assets/Square150x150Logo.scale-200.png";
        }

        public void AddCategory(Category cat)
        {
            cat.Owner = this;
            Categories.Add(cat);
        }

        public DateTime Date_Created
        {
            get; set;
        }

        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get; set;
        }
    }
}
