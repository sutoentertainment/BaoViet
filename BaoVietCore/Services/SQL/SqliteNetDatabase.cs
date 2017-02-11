using BaoVietCore.Interfaces;
using SQLite.Net;
using SQLite.Net.Platform.WinRT;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using BaoVietCore.Models;

namespace BaoVietCore.Services
{
    public class SqliteNetDatabaseBase : ServiceBase
    {
        public const string DATABASENAME = "BaoViet.sqlite";

        private static SQLiteConnection DbConnection(string DATABASENAME)
        {
            return new SQLiteConnection(
                new SQLitePlatformWinRT(),
                Path.Combine(ApplicationData.Current.LocalFolder.Path, DATABASENAME))
            { TraceListener = new DebugTraceListener() };
        }

        public SqliteNetDatabaseBase(Manager man) : base(man)
        {
        }

        public void CreateTable<T>()
        {
            using (var Db = DbConnection(DATABASENAME))
            {
                // Database stuff.
                // Create the table if it does not exist 
                var c = Db.CreateTable<T>();
                //var info = db.GetMapping(typeof(Item));
            }
        }

        public IEnumerable<T> GetItems<T>(bool ordered = true) where T : class, ISQLItem
        {
            using (var Db = DbConnection(DATABASENAME))
            {
                if (ordered)
                    return (from p in Db.Table<T>()
                            orderby p.Date_Created
                            select p).ToList();
                else
                    return (from p in Db.Table<T>()
                            select p).ToList();
            }
        }

        public void AddItem<T>(T item) where T : ISQLItem
        {
            using (var Db = DbConnection(DATABASENAME))
            {
                Db.Insert(item, typeof(T));
            }
        }

        public void InsertOrReplace<T>(T item) where T : ISQLItem
        {
            using (var Db = DbConnection(DATABASENAME))
            {
                Db.InsertOrReplace(item, typeof(T));
            }
        }

        public void Delete<T>(T item) where T : ISQLItem
        {
            using (var Db = DbConnection(DATABASENAME))
            {
                Db.Delete(item);
            }
        }

        public T GetItem<T>(int id) where T : class, ISQLItem
        {
            using (var Db = DbConnection(DATABASENAME))
            {
                return Db.Get<T>(id);
            }
        }

        /// <summary>
        /// Clear connection
        /// </summary>
        public void ClearUp()
        {
            //Db.Close();
        }

        public void OpenConnection()
        {
            //Db = DbConnection(DATABASENAME);
        }
    }

    public class SqliteNetDatabase : SqliteNetDatabaseBase, IDatabase
    {
        public SqliteNetDatabase(Manager man) : base(man)
        {
        }

        public void Configurate()
        {
            this.CreateTable<FeedItem>();
            this.CreateTable<CustomPaper>();
        }

        public IEnumerable<CustomPaper> GetCustomPaper()
        {
            return this.GetItems<CustomPaper>();
        }

        public IEnumerable<FeedItem> GetFeedItem()
        {
            return this.GetItems<FeedItem>();
        }

        public void AddFeedItem(FeedItem model)
        {
            this.AddItem<FeedItem>(model);
        }

        public void DeleteFeed(FeedItem feed)
        {
            this.Delete<FeedItem>(feed);
        }

        public void DeletePaper(CustomPaper p)
        {
            this.Delete<CustomPaper>(p);
        }

        public void AddPaper(CustomPaper p)
        {
            this.AddItem<CustomPaper>(p);
        }
    }
}
