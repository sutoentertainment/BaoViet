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

namespace BaoVietCore.Services
{
    public class Database : ServiceBase, IDatabase
    {
        public const string DATABASENAME = "BaoViet.sqlite";

        private static SQLiteConnection DbConnection(string DATABASENAME)
        {
            return new SQLiteConnection(
                new SQLitePlatformWinRT(),
                Path.Combine(ApplicationData.Current.LocalFolder.Path, DATABASENAME))
            {  TraceListener = new DebugTraceListener()};
        }

        public Database(Manager man) : base(man)
        {
        }

        public void CreateTable<T>()
        {
            using (var Db = DbConnection(DATABASENAME)){
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
                if(ordered)
                    return (from p in Db.Table<T>() orderby p.Date_Created
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
}
