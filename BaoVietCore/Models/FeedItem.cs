using BaoVietCore.Interfaces;
using GalaSoft.MvvmLight;
using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoVietCore.Models
{
    [Table("FeedItems")]
    public class FeedItem : ObservableObject, IFeedItem
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
                Set(ref _Title, value);
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
                Set(ref _Description, value);
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
                Set(ref _Link, value);
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
                Set(ref _Thumbnail, value);
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
