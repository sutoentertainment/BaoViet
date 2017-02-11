using BaoVietCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoVietCore.Interfaces
{
    public interface IDatabase
    {

        void Configurate();

        IEnumerable<CustomPaper> GetCustomPaper();
        IEnumerable<FeedItem> GetFeedItem();
        void AddFeedItem(FeedItem model);
        void DeleteFeed(FeedItem feed);
        void DeletePaper(CustomPaper p);
        void AddPaper(CustomPaper p);
    }
}
