//using BaoVietCore.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Diagnostics.Contracts;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Windows.Storage;
//using Microsoft.Data.Sqlite.Internal;
//using Microsoft.Data.Sqlite;

//namespace BaoVietCore.Services
//{
//    public class NativeSqliteDatabase : IDatabase
//    {
//        string databaseName;

//        private static SqliteConnection DbConnection(string DATABASENAME)
//        {
//            Contract.Ensures(!string.IsNullOrEmpty(DATABASENAME));
//            return new SqliteConnection(Path.Combine(ApplicationData.Current.LocalFolder.Path, DATABASENAME));
//        }

//        public void Configure(string dbName)
//        {
//            Contract.Ensures(!string.IsNullOrEmpty(dbName));
//            databaseName = dbName;

//            SqliteEngine.UseWinSqlite3();
//        }

//        /// <summary>
//        /// Must call <see cref="Database.Configure(string)"/> before.
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        public void CreateTable<T>()
//        {
//            using (var Db = DbConnection(databaseName))
//            {
//                // Database stuff.
//                // Create the table if it does not exist 
//                var c = Db.CreateTable<T>();
//                //var info = db.GetMapping(typeof(Item));
//            }
//        }

//        public IEnumerable<T> GetItems<T>(bool ordered = true) where T : class, ISQLItem
//        {
//            using (var Db = DbConnection(databaseName))
//            {
//                if (ordered)
//                    return (from p in Db.Table<T>()
//                            orderby p.Date_Created
//                            select p).ToList();
//                else
//                    return (from p in Db.Table<T>()
//                            select p).ToList();
//            }
//        }

//        public void AddItem<T>(T item) where T : ISQLItem
//        {
//            using (var Db = DbConnection(databaseName))
//            {
//                Db.InsertOrReplace(item, typeof(T));
//            }
//        }

//        public void Delete<T>(T item) where T : ISQLItem
//        {
//            using (var Db = DbConnection(databaseName))
//            {
//                Db.Delete(item);
//            }
//        }

//        public T GetItem<T>(int id) where T : class, ISQLItem
//        {
//            using (var Db = DbConnection(databaseName))
//            {
//                return Db.Get<T>(id);
//            }
//        }

//        /// <summary>
//        /// Clear connection
//        /// </summary>
//        public void ClearUp()
//        {
//            //Db.Close();
//        }

//        public void OpenConnection()
//        {
//            //Db = DbConnection(DATABASENAME);
//        }
//    }
//}
