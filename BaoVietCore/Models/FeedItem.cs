using BaoVietCore.Interfaces;
using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoVietCore.Models
{
    [Table("FeedItems")]
    public class FeedItem : BaseModel, ISQLItem
    {
        string _Title = "";

        public string Title
        {
            get
            {
                return _Title;
            }
            set
            {
                _Title = value;
                RaisePropertyChanged("Title");
            }
        }

        string _Description = "";
        public string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                _Description = value;
                RaisePropertyChanged("Description");
            }
        }

        string _Link = "";
        [Unique]
        public string Link
        {
            get
            {
                return _Link;
            }
            set
            {
                _Link = value;
                RaisePropertyChanged("Link");
            }
        }

        string _Thumbnail = "";
        public string Thumbnail
        {
            get
            {
                return _Thumbnail;
            }
            set
            {
                _Thumbnail = value;
                RaisePropertyChanged("Thumbnail");
            }
        }

        public bool IsImageAvailable
        {
            get
            {
                return !string.IsNullOrEmpty(Thumbnail);

            }
        }

        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get; set;
        }

        public DateTime Date_Created
        {
            get; set;
        } = DateTime.UtcNow;
    }
}
