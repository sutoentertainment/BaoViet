using SQLite.Net.Attributes;
using System;

namespace BaoVietCore.Interfaces
{
    public interface IFeedItem : ISQLItem
    {
        string Description { get; set; }
        bool IsImageAvailable { get; }
        string Link { get; set; }
        string Thumbnail { get; set; }
        string Title { get; set; }
    }
}